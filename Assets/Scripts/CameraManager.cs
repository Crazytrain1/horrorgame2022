using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject  Camera1;
    public GameObject Camera2;
    public GameObject Camera3;
    public GameObject Camera4;
    public GameObject Camera5;
    
    public GameObject Player;
    public Transform PlayerPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        Camera1.SetActive(true);
        Camera2.SetActive(false);
        Camera3.SetActive(false);
        Camera4.SetActive(false);
        Camera5.SetActive(false);
        

    }

    // Update is called once per frame
    void Update()
    {
     
    }
  
   
}
