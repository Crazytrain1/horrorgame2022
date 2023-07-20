using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{

    [SerializeField] GameObject porte;
    [SerializeField] GameObject punchReplacement;
    [SerializeField] float TheDistance;
    [SerializeField] GameObject InteractDisplayObject;
    [SerializeField] AudioSource _punchSound;
    private InteractDisplay _InteractDisplay;
    private DoorControll _porte;
    private int _DistanceMax = 2;
    private bool _CanInteract;

    public void Start()
    {
        _InteractDisplay = InteractDisplayObject.GetComponent<InteractDisplay>();
        

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

                _InteractDisplay.SetInteractDisplay("[E]", null, "Clock in");


            }
            else
            {
                _InteractDisplay.UpdateInteractDisplay();

            }
            if (Input.GetKeyDown("e") && TheDistance <= _DistanceMax)
            {
                Debug.Log("punching");
                _porte.ClockIn();
                _punchSound.Play();
                _CanInteract= false;
                _InteractDisplay.UpdateInteractDisplay();
                _InteractDisplay.RemoveObjective("I must clock in");


                punchReplacement.SetActive(true);
                this.gameObject.SetActive(false);
               

            }
        }
    }

    void OnMouseExit()
    {
        _InteractDisplay.UpdateInteractDisplay();
    }




}
