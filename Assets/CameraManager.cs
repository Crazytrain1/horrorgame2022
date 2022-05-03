using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject  Camera1;
    public GameObject Camera2;
    public GameObject Player;
    public Transform PlayerPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        Camera1.SetActive(true);
        Camera2.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.current.tag == "Moving")
        {
            Camera.current.transform.LookAt(PlayerPosition);
        }
    }
  
   
}
