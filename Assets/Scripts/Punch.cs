using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{

    [SerializeField] GameObject porte;
    [SerializeField] float TheDistance;
    [SerializeField] GameObject InteractDisplayObject;
    [SerializeField] Renderer rend;
    [SerializeField] Shader shader;
    [SerializeField] AudioSource _punchSound;
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

        rend = GetComponent<Renderer>();
        shader = Shader.Find("Universal Render Pipeline/Lit");

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
                _porte.ClockIn();
                _punchSound.Play();
                _CanInteract= false;
                _InteractDisplay.UpdateInteractDisplay();
                rend.material.shader = shader;

            }
        }
    }

    void OnMouseExit()
    {
        _InteractDisplay.UpdateInteractDisplay();
    }




}
