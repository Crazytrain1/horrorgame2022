using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{

    [SerializeField] Journal journal;
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
        this.GetComponent<Animation>().Play("clockFalling");
    }
}
