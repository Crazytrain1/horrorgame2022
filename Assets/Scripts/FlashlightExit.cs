using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FlashlightExit : MonoBehaviour
{

    [SerializeField] GameObject InteractDisplayObject;
    private InteractDisplay _InteractDisplay;
    [SerializeField] GameObject boxCollider;
    public InventoryItemData referenceItem;

    void Start()
    {
        _InteractDisplay = InteractDisplayObject.GetComponent<InteractDisplay>();
    }


    

    private void gotFlashlight()
    {
        boxCollider.SetActive(false);
        Destroy(this);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && InventorySystem.current.Get(referenceItem) == null)
        {
             _InteractDisplay.SetInteractDisplay(null, "I must grab the flashlight first", null);
            
        }
        else
        {
            gotFlashlight();
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
