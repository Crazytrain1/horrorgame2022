using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GhoulEvent : MonoBehaviour
{

    private bool eventDone = false;

    [SerializeField] PlayableDirector _Director;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !eventDone)
        {
            // big epic gamer moment ghoul
            Debug.Log("Ghoul Event");
            _Director.Play();
            eventDone = true;
        }
    }
}
