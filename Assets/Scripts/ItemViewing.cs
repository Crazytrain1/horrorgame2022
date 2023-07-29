using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemViewing : MonoBehaviour
{
    [SerializeField] GameObject item;
    [SerializeField] GameObject IU;
    [SerializeField] GameObject InteractCamera;
    [SerializeField] float TheDistance;
    [SerializeField] GameObject InteractDisplayObject;

    private InteractDisplay _InteractDisplay;
    private bool _open;
    private int _DistanceMax = 2;
    private bool _CanClose = false;





    public void Start()
    {
        if (InteractDisplayObject != null)
        {
            _InteractDisplay = InteractDisplayObject.GetComponent<InteractDisplay>();
            _InteractDisplay.UpdateInteractDisplay();
        }



    }
    private void Update()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;

        if (Input.GetKeyDown("e") && GameManager.Instance.State == GameManager.GameState.interacting && _open && _CanClose)
        {


            GameManager.Instance.UpdateGameState(GameManager.GameState.Playing);
            item.SetActive(false);
            IU.SetActive(false);
            InteractCamera.SetActive(false);
            _open = false;

        }
    }
    private void OnMouseOver()
    {
        if (TheDistance <= _DistanceMax)
        {

            _InteractDisplay.SetInteractDisplay("[E]", null, "Read journal");

        }
        else
        {
            _InteractDisplay.UpdateInteractDisplay();

        }
        if (Input.GetKeyDown("e") && TheDistance <= _DistanceMax && !_open)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.interacting);

            item.SetActive(true);
            IU.SetActive(true);
            InteractCamera.SetActive(true);
            _open = true;
            _CanClose = false;
            StartCoroutine("Delay");

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
