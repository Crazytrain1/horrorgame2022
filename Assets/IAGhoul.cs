using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum State{
    Roaming, Chasing, Searching, Kill,  LastSeen
}

public class IAGhoul : MonoBehaviour
{

    [SerializeField] float lookDistance = 3; 

    private State currentState;
    RaycastHit Hit;
    private Vector3 playerPosition;
    private bool giveUp =false;
    private bool startedCoroutine = false;
    private IEnumerator lastSeenCoroutine;

    void Start()
    {
        lastSeenCoroutine = null;
        currentState= State.Roaming;
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


    private void Roaming()
    {

        
        if (SeePlayer())
        {

            currentState = State.Chasing;
        }
        else
        {
            //Move(closest path)
            //Move(Path)
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


    private bool SeePlayer()
    {

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Hit, lookDistance) && Hit.transform.tag == "Player")
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * Hit.distance, Color.yellow);

            
            playerPosition = Hit.transform.position;
            Debug.Log(playerPosition.ToString());
            return true;

        }
        Debug.Log(Hit.transform.tag);
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

