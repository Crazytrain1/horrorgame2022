using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarAEvent : MonoBehaviour
{
    [SerializeField] Pillar Pillar;
    [SerializeField] GameObject door;
    void Awake()
    {

        Pillar = GetComponent<Pillar>();
        Pillar.pillarA += OpenDoor;
        
    }

    private void OnDestroy()
    {
        Pillar.pillarA -= OpenDoor;
    }


    private void OpenDoor()
    {
        Debug.Log("Pillar A event ");
        door.SetActive(false);
    }

}
