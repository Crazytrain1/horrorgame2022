using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeLightReminder : MonoBehaviour
{
    [SerializeField] GameObject InteractDisplayObject;
    private InteractDisplay _InteractDisplay;
    [SerializeField] GameObject boxCollider;
    [SerializeField] braker braker;
    private void Awake()
    {
        braker.breakerclosed += breakerOff;
    }
    void Start()
    {
        _InteractDisplay = InteractDisplayObject.GetComponent<InteractDisplay>();
    }
    private void OnDestroy()
    {
        braker.breakerclosed -= breakerOff;
    }



    private void breakerOff()
    {
        braker.breakerclosed -= breakerOff;
        boxCollider.SetActive(false);
        Destroy(this);

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
