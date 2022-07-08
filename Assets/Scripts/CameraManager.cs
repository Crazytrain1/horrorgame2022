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
        

    }

    // Update is called once per frame
    void Update()
    {
     
    }
  
   
}
