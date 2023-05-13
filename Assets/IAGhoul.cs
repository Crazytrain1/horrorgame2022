using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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
    private bool giveUp =false;
    private IEnumerator lastSeenCoroutine;

    RaycastHit Hit;

    NavMeshAgent agent;
    public Transform[] waypoints;
    Vector3 target;
    int waypointIndex;

    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        playerRef = GameObject.FindGameObjectWithTag("Player");
        lastSeenCoroutine = null;
        currentState= State.Roaming;
        UpdateDestination();
    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.Instance.State == GameManager.GameState.Playing)
        {



         
            switch (currentState)
            {
                case State.Roaming:
                    Roaming();
                    break;

                case State.Chasing:
                    Chasing();
                    break;

                case State.Searching:
                    Searching();
                    break;

                case State.LastSeen:
                    LastSeen();
                    break;

                case State.Kill:
                    Kill();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(State), currentState, null);
            }

            Debug.Log(currentState);
        }
    }

    void UpdateDestination()
    {
        target = waypoints[waypointIndex].transform.position;
        agent.SetDestination(target);

    }
    void IterateWaypointIndex()
    {
        waypointIndex++;
        if(waypointIndex == waypoints.Length) 
        {
            waypointIndex = 0;
        }

    }

    private void Roaming()
    {

        
        if (SeePlayer())
        {

            currentState = State.Chasing;
        }
        else
        {
            //Move(closest path)
            if(Vector3.Distance(transform.position, target) < 1)
            {
                IterateWaypointIndex();
                UpdateDestination();
            }
        }
    }
    private void Chasing()
    {
        Debug.Log(SeePlayer());
        if (SeePlayer())
        {
            giveUp = false;
            //Move to playerPosition
            //look at player Position
            if (Hit.distance <= 3.00f)
            {
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

        if(SeePlayer())
        {
            if (lastSeenCoroutine != null)
            {
                StopCoroutine(lastSeenCoroutine);
                
                lastSeenCoroutine= null;
                
            }
            giveUp = false;
            currentState = State.Chasing;
            return;
        }
      
        else if (this.transform.position == playerPosition)
        {
            if(SeePlayer())
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
            giveUp= false;
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

    }


    public bool SeePlayer()
    {

        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);
        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget= (target.position - transform.position).normalized;

            if(Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {

                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask)) { 


                    //NEED TO GET PLAYER POSITION FOR LAST SEEN STATE   
                    //playerPosition = Hit.transform.position;
                    
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
        giveUp= true;
        lastSeenCoroutine = null;
    }
}

