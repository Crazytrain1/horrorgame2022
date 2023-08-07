using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemRotating : MonoBehaviour
{

    private Vector3 posLastFrame;
   [SerializeField] GameObject cameraView;
    [SerializeField] ItemViewing realObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!cameraView.activeInHierarchy) { 
            realObject.Close();
            
            
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            posLastFrame = Mouse.current.position.ReadValue();
        }
        if (Mouse.current.leftButton.isPressed)
        {
            var delta = (Vector3)Mouse.current.position.ReadValue() - posLastFrame;
            posLastFrame = Mouse.current.position.ReadValue();

            var axis = Quaternion.AngleAxis(-90f, new Vector3(0, 0, 1)) * delta;
            this.transform.rotation = Quaternion.AngleAxis(delta.magnitude * 0.1f, axis) * this.transform.rotation;
        }
    }

    

}
