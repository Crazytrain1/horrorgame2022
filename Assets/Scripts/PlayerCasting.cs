
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCasting : MonoBehaviour
{

    [SerializeField] float ToTarget;
    
  
    void Update()
    {
        RaycastHit Hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * Hit.distance, Color.yellow);
            Debug.Log("fuck you criss de chienne");
            ToTarget = Hit.distance;
           
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("criss de caliss pourquoi tas rien pogné");
        }

    }
}
