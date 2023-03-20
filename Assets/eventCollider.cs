using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventCollider : MonoBehaviour
{
    public event Action lightOff;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            lightOff?.Invoke();
        }
    }
}
