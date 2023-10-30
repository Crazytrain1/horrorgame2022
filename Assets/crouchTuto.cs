using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crouchTuto : MonoBehaviour
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
            _InteractDisplay.SetInteractDisplay("[C]", "I think i can duck under that", null);
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
