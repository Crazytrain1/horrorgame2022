using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pencil : MonoBehaviour
{

    [SerializeField] float TheDistance;
    [SerializeField] GameObject InteractDisplayObject;
    private InteractDisplay _InteractDisplay;
    private int _DistanceMax = 2;
    private bool rotating = false;

    public Quaternion targetRotation; 
    public float rotationSpeed = 2f;

    private List<int> currentHour = new List<int> { 330, 300, 270, 240, 210, 180, 150, 120, 90, 60, 30 ,0 };

    int hourIndex;
    [SerializeField] int rightPosition;
    public bool correct = false;
    // Start is called before the first frame update
    void Start()
    {
        _InteractDisplay = InteractDisplayObject.GetComponent<InteractDisplay>();
        _InteractDisplay.UpdateInteractDisplay();


    }

    // Update is called once per frame
    void Update()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;

        

        if(rotating && targetRotation!= null)
        {
            correct = false;
            Quaternion newRotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            transform.rotation = newRotation;
        }
        if(Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
        {
            rotating= false;
            if(hourIndex== rightPosition) 
            {
                correct = true;
            }
            else {
                correct = false;
            }
        }
    }

    private void OnMouseOver()
    {
        if (TheDistance <= _DistanceMax)
        {

            _InteractDisplay.SetInteractDisplay("[E]", null, "Rotate crayon");


        }
        else
        {
            _InteractDisplay.UpdateInteractDisplay();

        }
        if (Input.GetKeyDown("e") && TheDistance <= _DistanceMax)
        {
            rotating = true;
           
            targetRotation = Quaternion.Euler(currentHour[hourIndex], 0f, 0f);
            hourIndex++;
            if (hourIndex >= currentHour.Count)
            {
                hourIndex = 0;
            }
            
        }

    }

    void OnMouseExit()
    {
        _InteractDisplay.UpdateInteractDisplay();
    }


}


