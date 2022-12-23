using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Journal : MonoBehaviour
{
    [SerializeField] GameObject journal;
    [SerializeField] float TheDistance;
    [SerializeField] GameObject InteractDisplayObject;
    private InteractDisplay _InteractDisplay;
    private DoorControll _porte;
    private bool open;
    private int _DistanceMax = 2;

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

                _InteractDisplay.SetInteractDisplay(true, null, "Read journal");


            }
            else
            {
                _InteractDisplay.UpdateInteractDisplay();

            }
            if (Input.GetKeyDown("e") && TheDistance <= _DistanceMax && !open)
            {
            GameManager.Instance.UpdateGameState(GameManager.GameState.interacting);
                Debug.Log("reading");
            journal.SetActive(true);
            open = true;
            }
            else if (Input.GetKeyDown("e") && TheDistance <= _DistanceMax && open)
            {
                Debug.Log("closing");
                journal.SetActive(false);
                open = false;
            }
    }
    

    void OnMouseExit()
    {
        _InteractDisplay.UpdateInteractDisplay();
    }
}
