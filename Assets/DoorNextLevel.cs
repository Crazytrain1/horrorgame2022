using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DoorNextLevel : MonoBehaviour
{

    [SerializeField] float TheDistance;

    private InteractDisplay _InteractDisplay;
    private int _DistanceMax = 2;
    [SerializeField] GameObject InteractDisplayObject;
    void Start()
    {
        _InteractDisplay = InteractDisplayObject.GetComponent<InteractDisplay>();
        _InteractDisplay.UpdateInteractDisplay();


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

            _InteractDisplay.SetInteractDisplay(true, null, "enter Catacombs");


        }
        else
        {
            _InteractDisplay.UpdateInteractDisplay();

        }
        if (Input.GetKeyDown("e") && TheDistance <= _DistanceMax)
        {
            GameManager.Instance.UpdateLevel(GameManager.Level.Level1);
            _InteractDisplay.UpdateInteractDisplay();
        }

    }

    void OnMouseExit()
    {
        _InteractDisplay.UpdateInteractDisplay();
    }
}
