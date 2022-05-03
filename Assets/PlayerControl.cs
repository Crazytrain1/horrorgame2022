using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 5f;
    public ArrayList [] CameraTrigger;
    public GameObject CameraManager;
    
    // Update is called once per frame
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
            Debug.Log("ree");
            CameraManager.SendMessage("CameraChange");
        }
    }
}
