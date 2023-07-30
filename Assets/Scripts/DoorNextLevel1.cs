using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorNextLevel1 : MonoBehaviour
{

    [SerializeField] float TheDistance;
    [SerializeField] GameObject InteractDisplayObject;

    private InventorySystem.InventoryItem InventoryItem;
    private InteractDisplay _InteractDisplay;

    private IDataService DataService = new JsonDataService();
    private int _DistanceMax = 2;

    public InventoryItemData referenceItem;
    

    void Start()
    {       
        if (InteractDisplayObject != null)
        {
            _InteractDisplay = InteractDisplayObject.GetComponent<InteractDisplay>();
            _InteractDisplay.UpdateInteractDisplay();
        }
    }

    
    void Update()
    {

        TheDistance = PlayerCasting.DistanceFromTarget;

    
    }

    private void OnMouseOver()
    {
        if (TheDistance <= _DistanceMax)
        {

            _InteractDisplay.SetInteractDisplay("[E]", null, "open door");
            

        }
        else
        {
            _InteractDisplay.UpdateInteractDisplay();

        }
        if (Input.GetKeyDown("e") && TheDistance <= _DistanceMax)
        {
            if (InventorySystem.current.Get(referenceItem) == null)
            {
                _InteractDisplay.SetInteractDisplay("[E]", "you need the key", "open door");
            }
            else
            {
                InventorySystem.current.Remove(referenceItem);
                GameManager.Instance.saveInventory();
                GameManager.Instance.saveLevel(2, 0);
                GameManager.Instance.UpdateLevel(GameManager.Level.Level2);
                _InteractDisplay.UpdateInteractDisplay();
            }

        }

    }

    void OnMouseExit()
    {
        _InteractDisplay.UpdateInteractDisplay();
    }
}
