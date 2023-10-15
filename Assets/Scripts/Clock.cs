using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{

    [SerializeField] Journal journal;
    [SerializeField] AudioSource sound;
    [SerializeField] AudioClip breaking;
    [SerializeField] GameObject ground;
    void Awake(){
        journal.ClockFall += clockFalling;
    }

    private void OnDestroy()
    {
        journal.ClockFall -= clockFalling;
    }


    void clockFalling()
    {
        journal.ClockFall -= clockFalling;
        this.gameObject.GetComponent<Rigidbody>().useGravity = true;
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == ground) 
        {
            this.GetComponent<Animation>().Play("clockFalling");
        }
    }
    void glassBreaking()
    {
        sound.PlayOneShot(breaking);
    }
}
