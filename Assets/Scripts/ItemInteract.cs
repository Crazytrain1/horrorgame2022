using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemInteract : MonoBehaviour
{
    [SerializeField] int objectType = 1;
    [SerializeField] GameObject InteractDisplayObject;
    [SerializeField] float TheDistance;
    private InteractDisplay _InteractDisplay;
    private int _DistanceMax = 2;
    private GameObject realItem;

    public void Start()
    {
        _InteractDisplay = InteractDisplayObject.GetComponent<InteractDisplay>();
        _InteractDisplay.UpdateInteractDisplay();
        

    }

    private void Update()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;
    }

    private void OnMouseOver()
    {
        if (TheDistance <= _DistanceMax)
        {

            _InteractDisplay.SetInteractDisplay(true, null, "Pick up office keys");


        }
        else
        {
            _InteractDisplay.UpdateInteractDisplay();

        }
        if (Input.GetKeyDown("e") && TheDistance <= _DistanceMax)
        {

            if (objectType == 1)
            {
                realItem = this.gameObject;
                realItem.TryGetComponent<ItemObject>(out ItemObject Item);
                Item.OnPickupItem();
                _InteractDisplay.UpdateInteractDisplay();
            }
        }
    }

    void OnMouseExit()
    {
        _InteractDisplay.UpdateInteractDisplay();
    }


}
