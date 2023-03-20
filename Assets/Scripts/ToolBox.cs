using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBox : MonoBehaviour
{
    [SerializeField] GameObject qteSlider;
    [SerializeField] float TheDistance;
    [SerializeField] GameObject InteractDisplayObject;
    public bool CanInteract = true;
    private InteractDisplay _InteractDisplay;
    private bool _open;
    private int _DistanceMax = 2;
    private bool _CanClose = false;

    public void Start()
    {
        _InteractDisplay = InteractDisplayObject.GetComponent<InteractDisplay>();

        

    }
    private void Update()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;

        if (Input.GetKeyDown("e") && GameManager.Instance.State == GameManager.GameState.interacting && _open && _CanClose)
        {


            GameManager.Instance.UpdateGameState(GameManager.GameState.Playing);
            qteSlider.SetActive(false);
            _open = false;
        }
    }

    private void OnMouseOver()
    {
        if (CanInteract)
        {
            if (TheDistance <= _DistanceMax)
            {

                _InteractDisplay.SetInteractDisplay("[E]", null, "search toolbox");


            }
            else
            {
                _InteractDisplay.UpdateInteractDisplay();

            }
            if (Input.GetKeyDown("e") && TheDistance <= _DistanceMax && !_open)
            {
                GameManager.Instance.UpdateGameState(GameManager.GameState.interacting);

                qteSlider.SetActive(true);
                _open = true;
                _CanClose = false;
                StartCoroutine("Delay");

            }





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
