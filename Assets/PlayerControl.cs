using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 5f;
    public GameObject CameraManager;
    [SerializeField] cameraswitch cameraSwitch;
    public GameObject Trigger;
    int x;
    int y;

    // Update is called once per frame

    private void Start()
    {
        
    }
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.01f)
        {

            controller.Move(direction * speed * Time.deltaTime);
        }


        


    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "CameraTriggerEnter")
        {
            Trigger = other.gameObject;
            
            cameraSwitch = Trigger.GetComponent<cameraswitch>();
           
            if (!cameraSwitch.cameraExit.activeInHierarchy)
            {
                cameraSwitch.cameraEnter.SetActive(false);
                cameraSwitch.cameraExit.SetActive(true);
                Debug.Log("entre");

            }
        }
        if (other.gameObject.tag == "CameraTriggerExit")
        {
            Trigger = other.gameObject;

            cameraSwitch = Trigger.GetComponent<cameraswitch>();
            
            if (cameraSwitch.cameraExit.activeInHierarchy)
            {
                cameraSwitch.cameraEnter.SetActive(true);
                cameraSwitch.cameraExit.SetActive(false);
                Debug.Log("Sort");
                
            }
        }
    }
   
}
