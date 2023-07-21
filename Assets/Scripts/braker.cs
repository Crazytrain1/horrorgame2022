using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class braker : MonoBehaviour
{

    [SerializeField] GameObject InteractDisplayObject;
    [SerializeField] float TheDistance;
    [SerializeField] GameObject[] lights;


    private InteractDisplay _InteractDisplay;
    private bool eventDone = false;

    public event Action breakerclosed;

    private int _DistanceMax = 2;
    [SerializeField] PlayableDirector _Director;
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
        if (TheDistance <= _DistanceMax &&  !eventDone )
        {

            _InteractDisplay.SetInteractDisplay("[E]", null, "Turn off light");


        }
        else
        {
            _InteractDisplay.UpdateInteractDisplay();

        }
        if (Input.GetKeyDown("e") && TheDistance <= _DistanceMax && !eventDone)
        {
            _InteractDisplay.RemoveObjective("I must close the breaker, it's pricey to let the electricity open during the night");
            _InteractDisplay.UpdateObjective("Now I need to patrol the catacombs",0);
            eventDone = true;
            _Director.Play();
            breakerclosed?.Invoke();
            foreach ( GameObject lightbulb in lights)
            {
                lightbulb.SetActive(false);
            }

            

        }



    }

    void OnMouseExit()
    {
        _InteractDisplay.UpdateInteractDisplay();
    }
}
