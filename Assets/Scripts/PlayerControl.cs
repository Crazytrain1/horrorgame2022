using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    
    public ItemObject Item;
   
    public GameObject CameraManager;
    [SerializeField] cameraswitch cameraSwitch;
    public GameObject Trigger;
    public GameObject realItem;

    // Update is called once per frame

  
    void Update()
    {
     


        


    }
    private void OnTriggerEnter(Collider other)
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
        if (other.gameObject.tag == "Crawl")
        {
            Trigger = other.gameObject;
            InteractableObject.crouching = false;


            Debug.Log("sort");
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("testfunction");
        if (collision.gameObject.tag == "Item")
        {
            realItem = collision.gameObject;
            realItem.TryGetComponent<ItemObject>(out ItemObject Item);
            Debug.Log("marche a moitié");
            Item.OnPickupItem();
            
            
        }
    }
}
