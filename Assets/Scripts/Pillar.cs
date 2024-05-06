using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;




public class Pillar : MonoBehaviour
{
    public event Action pillarA;
    public event Action pillarB;
    public event Action pillarC;

    [SerializeField] float TheDistance;
    [SerializeField] GameObject InteractDisplayObject;
    [SerializeField] String itemName;
    [SerializeField] int pillarEvent;

    private InteractDisplay _InteractDisplay;
    
    private int _DistanceMax = 2;
    private bool _CanInteract;

    public InventoryItemData referenceItem;

    


    public void Start()
    {
        _InteractDisplay = InteractDisplayObject.GetComponent<InteractDisplay>();

      


        
        _CanInteract = true;

    }

    private void Update()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;
    }


    private void OnMouseOver()
    {
        if (_CanInteract)
        {
            if (TheDistance <= _DistanceMax)
            {



                if (InventorySystem.current.Get(referenceItem) == null)
                {
                    _InteractDisplay.SetInteractDisplay(null, "I think i need to place something there", null);
                }
                else
                {
                    _InteractDisplay.SetInteractDisplay("[E]", null, "Place" + itemName);
                }



            }




        }
        else
        {
            _InteractDisplay.UpdateInteractDisplay();

        }
        if (Input.GetKeyDown("e") && TheDistance <= _DistanceMax)
        {
            Debug.Log("Placing item");
            _CanInteract = false;
            _InteractDisplay.UpdateInteractDisplay();
            InventorySystem.current.Remove(referenceItem);
            _InteractDisplay.RemoveObjective("what to do with this");

          switch (pillarEvent) {

                case 0:
                    pillarA?.Invoke();
                    break;
                case 1:
                    pillarB?.Invoke();
                    break;
                case 2:
                    pillarC?.Invoke();
                    break;
                default:
                    break;


            
            }


        }

    }


    void OnMouseExit()
    {
        _InteractDisplay.UpdateInteractDisplay();
    }
}
