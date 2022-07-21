using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject  CameraBureau;
    public GameObject CameraHallway;
    public GameObject CameraWaitingRoom;
    public GameObject CameraBathroom;
    public GameObject CameraWorkstation;
    public GameObject CameraHallwayTransition1;
    public GameObject CameraHallwayTransition2;
    public GameObject CameraHallwayTransition3;
    public GameObject CameraHallwayTransition4;
    public GameObject CameraStaircase1;
    public GameObject CameraStaircase2;


    public GameObject Player;
    public Transform PlayerPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        CameraBureau.SetActive(true);
        CameraHallway.SetActive(false);
        CameraWaitingRoom.SetActive(false);
        CameraBathroom.SetActive(false);
        CameraWorkstation.SetActive(false);
        CameraHallwayTransition1.SetActive(false);
        CameraHallwayTransition2.SetActive(false);
        CameraHallwayTransition3.SetActive(false);
        CameraHallwayTransition4.SetActive(false);
        CameraStaircase1.SetActive(false);
        CameraStaircase2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
     
    }
  
   
}
