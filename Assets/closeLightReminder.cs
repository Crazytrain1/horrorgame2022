using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeLightReminder : MonoBehaviour
{
    [SerializeField] GameObject InteractDisplayObject;
    private InteractDisplay _InteractDisplay;
    void Start()
    {
        _InteractDisplay = InteractDisplayObject.GetComponent<InteractDisplay>();
    }

 
   
        
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _InteractDisplay.SetInteractDisplay(null, "Close the light before continuing", null);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _InteractDisplay.UpdateInteractDisplay();
        }
    }


}
