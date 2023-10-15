using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;



public class Journal : MonoBehaviour
{

    public event Action ClockFall;

    [SerializeField] GameObject journal;
    [SerializeField] float TheDistance;
    [SerializeField] GameObject InteractDisplayObject;
    [SerializeField] TextMeshProUGUI messageText;
    [SerializeField] string displaymessage;
    [SerializeField] DoorControll door;

    public InventoryItemData referenceItem;

    private InteractDisplay _InteractDisplay;
    private bool _open;
    private int _DistanceMax = 2;
    private bool _CanClose = false;
    private bool _firstTime = true;
    private List<string> task = new List<string>();
   
    
    


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
            journal.SetActive(false);
            _open = false;
            ClockFall?.Invoke();

            if (InventorySystem.current.Get(referenceItem) == null)
            {
                task.Add("I must find the keys");
            }

            if (door.notclockIn)
            {
                task.Add("I must clock in");
            }

            task.Add("I need to leave the office");
            if (_firstTime)
            {
                for (int i = 0; i < task.Count; i++)
                {
                    _InteractDisplay.UpdateObjective(task[i], i);
                    _firstTime = false;
                }
            }
        }
    }
    private void OnMouseOver()
    {
        if (TheDistance <= _DistanceMax)
        {

            _InteractDisplay.SetInteractDisplay("[E]", null, "Read journal");
            messageText.SetText(displaymessage);


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
