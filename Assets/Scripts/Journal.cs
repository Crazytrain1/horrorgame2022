using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private bool _CanClose = false;




    public void Start()
    {
        _InteractDisplay = InteractDisplayObject.GetComponent<InteractDisplay>();
        _InteractDisplay.UpdateInteractDisplay();

    }
    private void Update()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;

        if (Input.GetKeyDown("e") && GameManager.Instance.State == GameManager.GameState.interacting && _open && _CanClose)
        {


            GameManager.Instance.UpdateGameState(GameManager.GameState.Playing);
            journal.SetActive(false);
            _open = false;
        }
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
            _CanClose= false;
            StartCoroutine("Delay");
            Debug.Log("fuck off");
        }
      


    }

    IEnumerator Delay()
    {
        yield return new WaitForEndOfFrame();
        _CanClose = true;
    }

    void OnMouseExit()
    {
        _InteractDisplay.UpdateInteractDisplay();
    }
}