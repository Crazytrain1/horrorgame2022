using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;
enum State{
    Roaming, Chasing, Searching, Kill,  LastSeen
}

public class IAGhoul : MonoBehaviour
{




    public float radius;
    [Range(0, 360)]
    public float angle;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    private State currentState;

    private Vector3 playerPosition;
    private bool giveUp = false;
    private IEnumerator lastSeenCoroutine;
    private bool isKilled = false;

    [SerializeField] float killRange;
    [SerializeField] float distanceToTarget;

    [SerializeField] PlayableDirector _Director;

    [SerializeField] Animator animator;


    RaycastHit Hit;

    NavMeshAgent agent;
    public Transform[] waypoints;
    Vector3 WaypointTarget;
    int waypointIndex;

    private Vector2 Velocity;
    private Vector2 SmoothDeltaPosition;

    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        playerRef = GameObject.FindGameObjectWithTag("Player");
        lastSeenCoroutine = null;
        animator.applyRootMotion = true;
        agent.updatePosition = false;
        agent.updateRotation = true;
        currentState = State.Roaming;
        UpdateDestination();
    }

    private void OnAnimatorMove()
    {
        Vector3 rootPosition = agent.nextPosition;
        rootPosition.y = agent.nextPosition.y;
        transform.position = rootPosition;
        agent.nextPosition = rootPosition;
    }

    private void SynchronizeAnimatorAndAgent()
    {
        Vector3 worldDeltaPosition = agent.nextPosition - transform.position;
        worldDeltaPosition.y = 0;

        float dx = Vector3.Dot(transform.right, worldDeltaPosition);
        float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
        Vector2 deltaPosition = new Vector2(dx, dy);

        float smooth = Mathf.Min(1, Time.deltaTime / 0.1f);
        SmoothDeltaPosition = Vector2.Lerp(SmoothDeltaPosition, deltaPosition, smooth);

        Velocity = SmoothDeltaPosition / Time.deltaTime;
        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            Velocity = Vector2.Lerp(
                Vector2.zero,
                Velocity, 
                agent.remainingDistance / agent.stoppingDistance
                );
        }

        animator.SetFloat("locomotion", Velocity.magnitude);
        

        float deltaMagnitude = worldDeltaPosition.magnitude;
        if( deltaMagnitude > agent.radius / 2f )
        {
            transform.position = Vector3.Lerp(
                animator.rootPosition,
                agent.nextPosition,
                smooth
                );
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.Instance.State == GameManager.GameState.Playing)
        {




            switch (currentState)
            {
                case State.Roaming:
                    animator.SetBool("Roaming", true);
                    animator.SetBool("Chasing", false);
                    animator.SetBool("LastSeen", false);
                    
                    Roaming();
                    break;

                case State.Chasing:

                    animator.SetBool("Roaming", false);
                    animator.SetBool("Chasing", true);
                    animator.SetBool("LastSeen", false);
                    Chasing();
                    break;

                case State.Searching:

                    animator.SetBool("Roaming", false);
                    animator.SetBool("Chasing", false);
                    animator.SetBool("LastSeen", true);
                    Searching();
                    break;

                case State.LastSeen:

                    animator.SetBool("Roaming", false);
                    animator.SetBool("Chasing", false);
                    animator.SetBool("LastSeen", true);

                    LastSeen();
                    break;

                case State.Kill:

                    animator.SetBool("Roaming", false);
                    animator.SetBool("Chasing", false);
                    animator.SetBool("LastSeen", false);

                    Kill();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(State), currentState, null);
            }

            Debug.Log(currentState);
        }
        SynchronizeAnimatorAndAgent();
    }

    void UpdateDestination()
    {
        //need to only update when in Roaming mode
        WaypointTarget = waypoints[waypointIndex].transform.position;
        agent.SetDestination(WaypointTarget);

    }
    void IterateWaypointIndex()
    {
        waypointIndex++;
        if (waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
        }

    }

    private void Roaming()
    {

        agent.speed = 3f;
        if (SeePlayer())
        {

            currentState = State.Chasing;
        }
        else
        {



            if (Vector3.Distance(transform.position, WaypointTarget) < 2)
            {
                IterateWaypointIndex();
                UpdateDestination();
            }
            UpdateDestination();



        }
    }
    private void Chasing()
    {
        Debug.Log(SeePlayer());
        agent.speed = 4f;
        if (SeePlayer())
        {
            giveUp = false;
            agent.SetDestination(playerPosition);

            if (distanceToTarget <= killRange)
            {
                agent.isStopped = true;
                currentState = State.Kill;
            }
        }
        else
        {

            currentState = State.LastSeen;
        }
    }
    private void Searching()
    {
        //animation cool look around

        if (SeePlayer())
        {

            currentState = State.Chasing;
        }
        else
        {
            currentState = State.Roaming;
        }
    }

    private void LastSeen()
    {
        //Move to playerPosition


        //look at playerPosition

        if (SeePlayer())
        {
            if (lastSeenCoroutine != null)
            {
                StopCoroutine(lastSeenCoroutine);

                lastSeenCoroutine = null;

            }
            giveUp = false;
            currentState = State.Chasing;
            return;
        }

        else if (this.transform.position == playerPosition)
        {
            if (SeePlayer())
            {
                if (lastSeenCoroutine != null)
                {
                    StopCoroutine(lastSeenCoroutine);
                    lastSeenCoroutine = null;

                }
                currentState = State.Chasing;
                return;
            }
            else
            {
                if (lastSeenCoroutine != null)
                {
                    StopCoroutine(lastSeenCoroutine);
                    lastSeenCoroutine = null;

                }
                currentState = State.Searching;
                return;
            }
        }
        if (giveUp)
        {
            giveUp = false;
            lastSeenCoroutine = null;

            currentState = State.Roaming;
            return;
        }
        if (lastSeenCoroutine == null)
        {
            lastSeenCoroutine = StopChaseDebug();
            StartCoroutine(lastSeenCoroutine);
        }



    }



    private void Kill()
    {
        Debug.Log("get gud scrub");
        if (!isKilled)
        {
            StartCoroutine(PlayTimelineCoroutine(_Director));
        }
        isKilled = true;


    }


    public bool SeePlayer()
    {

        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);
        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {

                distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {



                    playerPosition = target.position;

                    return true;

                }
                else
                    return false;
            }
            else
                return false;
        }
        return false;

    }

    IEnumerator StopChaseDebug()
    {
        giveUp = false;
        yield return new WaitForSeconds(5f);
        giveUp = true;
        lastSeenCoroutine = null;
    }

    IEnumerator PlayTimelineCoroutine(PlayableDirector _Director)
    {
        _Director.Play();
        yield return new WaitForSeconds((float)_Director.duration);
        GameManager.Instance.UpdateGameState(GameManager.GameState.death);

    }
}

