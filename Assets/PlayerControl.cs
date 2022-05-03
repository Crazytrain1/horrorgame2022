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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CameraTrigger")
        {
            Trigger = other.gameObject;
            cameraSwitch = Trigger.GetComponent<cameraswitch>();
            if (cameraSwitch.cameraExit.activeInHierarchy)
            {
                x = 1;
            }
            else if (!cameraSwitch.cameraExit.activeInHierarchy)
            {
                y = 1;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "CameraTrigger")
        {
            Trigger = other.gameObject;
            cameraSwitch = Trigger.GetComponent<cameraswitch>();
            if (x==1)
            {
                cameraSwitch.cameraExit.SetActive(false);
                cameraSwitch.cameraEnter.SetActive(true);
                Debug.Log("sort");
                x = 0;
            }
            else if(y==1)
                {
                cameraSwitch.cameraEnter.SetActive(false);
                cameraSwitch.cameraExit.SetActive(true);
                Debug.Log("entre");
                y = 0;
            }
           
            Debug.Log("ree");
            
        }
    }
}
