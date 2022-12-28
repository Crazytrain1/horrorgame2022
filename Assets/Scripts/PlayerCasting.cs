
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCasting : MonoBehaviour
{

    public static float DistanceFromTarget;
    public float ToTarget;




    void Update()
    {
        if (GameManager.Instance.State == GameManager.GameState.Playing)
            

        {
            RaycastHit Hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Hit, Mathf.Infinity))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * Hit.distance, Color.yellow);

                ToTarget = Hit.distance;
                DistanceFromTarget = ToTarget;

            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);

            }
        }
        else
        {
            DistanceFromTarget = 100;

        }

    }
}
