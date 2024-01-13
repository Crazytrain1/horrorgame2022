using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crawlTuto : MonoBehaviour
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
            _InteractDisplay.SetInteractDisplay("[CTRL]", "It's tight, but i think i can crawl under that", null);
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
