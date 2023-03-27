using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class braker : MonoBehaviour
{

    [SerializeField] GameObject InteractDisplayObject;
    private InteractDisplay _InteractDisplay;
    [SerializeField] float TheDistance;
    private int _DistanceMax = 2;

    public void Start()
    {
        if (InteractDisplayObject != null)
        {
            _InteractDisplay = InteractDisplayObject.GetComponent<InteractDisplay>();
            _InteractDisplay.UpdateInteractDisplay();
        }

    }

    // Update is called once per frame
    void Update()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;
    }

    private void OnMouseOver()
    {
        if (TheDistance <= _DistanceMax)
        {

            _InteractDisplay.SetInteractDisplay("[E]", null, "Turn off light");


        }
        else
        {
            _InteractDisplay.UpdateInteractDisplay();

        }
        if (Input.GetKeyDown("e") && TheDistance <= _DistanceMax)
        {
            

        }



    }

    void OnMouseExit()
    {
        _InteractDisplay.UpdateInteractDisplay();
    }
}
