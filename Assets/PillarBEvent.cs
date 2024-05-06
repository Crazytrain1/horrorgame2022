using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class PillarBEvent : MonoBehaviour
{
    [SerializeField] Pillar Pillar;
    [SerializeField] GameObject door;
    void Awake()
    {

        Pillar = GetComponent<Pillar>();
        Pillar.pillarB += OpenDoor;

    }

    private void OnDestroy()
    {
        Pillar.pillarB -= OpenDoor;
    }


    private void OpenDoor()
    {
        Debug.Log("Pillar B event ");
        door.SetActive(false);
    }

}
