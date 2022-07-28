using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Camera Level 0")]
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
    public GameObject CameraBureau2;
    public GameObject CameraHallwayTop;

    [Header("Camera Level 1")]
    public GameObject CameraStair;
    public GameObject CameraLongHallway;
    public GameObject CameraEmptyRoom;
    public GameObject CameraLongEmptyHallway;
    public GameObject CameraBackOfLoreRoom;
    public GameObject CameraSideOfLoreRoom;
    public GameObject CameraLoreRoom;
    public GameObject CameraCorridorEmpty;
    public GameObject CameraHugeRoom;
    public GameObject CameraOpenLoreRoom;
    public GameObject CameraHugeOpenLoreRoom;
    public GameObject CameraCorridorLeaving1;
    public GameObject CameraCorridorLeaving2;
    


    [SerializeField] enum Level { MainMenu, Level0, Level1, Level2, Level3, Level4 };
    [SerializeField] Level NextLevel;

    public GameObject Player;
    public Transform PlayerPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        if (NextLevel == Level.Level0)
        {
            CameraBureau.SetActive(false);
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
            CameraHallwayTop.SetActive(false);
            CameraBureau2.SetActive(true);
        }
        else if (NextLevel == Level.Level1)
        {
            CameraStair.SetActive(true);
            CameraLongHallway.SetActive(false);
            CameraEmptyRoom.SetActive(false);
            CameraLongEmptyHallway.SetActive(false);
            CameraBackOfLoreRoom.SetActive(false);
            CameraSideOfLoreRoom.SetActive(false);
            CameraLoreRoom.SetActive(false);
            CameraCorridorEmpty.SetActive(false);
            CameraHugeRoom.SetActive(false);
            CameraOpenLoreRoom.SetActive(false);
            CameraHugeOpenLoreRoom.SetActive(false);
            CameraCorridorLeaving1.SetActive(false);
            CameraCorridorLeaving2.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
     
    }
  
   
}
