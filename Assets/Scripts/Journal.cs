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
    private bool _open;
    private int _DistanceMax = 2;
    private bool _OnClose;

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
        if (Input.GetKeyDown("e") && TheDistance <= _DistanceMax && !_open)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.interacting);

            journal.SetActive(true);
            _open = true;
        }
       else  if (Input.GetKeyDown("e") && GameManager.Instance.State == GameManager.GameState.interacting && _open)
        {

            _OnClose = true;
            GameManager.Instance.UpdateGameState(GameManager.GameState.Playing);
            journal.SetActive(false);
            _open = false;
        }


    }

    

    void OnMouseExit()
    {
        _InteractDisplay.UpdateInteractDisplay();
    }
}
