using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{

    [SerializeField] GameObject porte;
    [SerializeField] float TheDistance;
    [SerializeField] GameObject InteractDisplayObject;
    private InteractDisplay _InteractDisplay;
    private DoorControll _porte;
    private int _DistanceMax = 2;
    private bool _CanInteract;

    public void Start()
    {
        _InteractDisplay = InteractDisplayObject.GetComponent<InteractDisplay>();
        _InteractDisplay.UpdateInteractDisplay();

        _porte = porte.GetComponent<DoorControll>();
        _CanInteract= true;

    }
    private void Update()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;
    }
    private void OnMouseOver()
    {if(_CanInteract)
        {
            if (TheDistance <= _DistanceMax)
            {

                _InteractDisplay.SetInteractDisplay(true, null, "Clock in");


            }
            else
            {
                _InteractDisplay.UpdateInteractDisplay();

            }
            if (Input.GetKeyDown("e") && TheDistance <= _DistanceMax)
            {
                Debug.Log("punching");
                _porte.SetLock(false);
                _CanInteract= false;
            }
        }
    }

    void OnMouseExit()
    {
        _InteractDisplay.UpdateInteractDisplay();
    }




}
