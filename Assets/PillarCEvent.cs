using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class PillarCEvent : MonoBehaviour
{
    [SerializeField] Pillar Pillar;
    [SerializeField] GameObject door;
    void Awake()
    {

        Pillar = GetComponent<Pillar>();
        Pillar.pillarC += OpenDoor;

    }

    private void OnDestroy()
    {
        Pillar.pillarC -= OpenDoor;
    }


    private void OpenDoor()
    {
        Debug.Log("Pillar C event ");
        door.SetActive(false);
    }

}