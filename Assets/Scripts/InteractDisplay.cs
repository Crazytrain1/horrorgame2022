using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InteractDisplay : MonoBehaviour
{
    [SerializeField] GameObject ActionDisplay;
    [SerializeField] GameObject ActionText;
    [SerializeField] GameObject Text;
    [SerializeField] GameObject MenuPause;
    [SerializeField] GameObject MenuDeath;
    [SerializeField] GameObject Objective;
    [SerializeField] GameObject FirstObjective;
    [SerializeField] GameObject TitleObjective;

    [SerializeField] TextMeshProUGUI objectiveText;


    private TextMeshProUGUI _Text;
    private TextMeshProUGUI _ActionText;
    private TextMeshProUGUI _InteractMessage;


    private void Awake()
    {
        GameManager.StateChanged += GameManager_StateChanged;
    }

    private void OnDestroy()
    {
        GameManager.StateChanged -= GameManager_StateChanged;
    }

    private void GameManager_StateChanged(GameManager.GameState State)
    {
        MenuPause.SetActive(State == GameManager.GameState.Pausing);
        TitleObjective.SetActive(State == GameManager.GameState.Pausing);
        FirstObjective.SetActive(State == GameManager.GameState.Pausing);
        Objective.SetActive(State == GameManager.GameState.Pausing);
        MenuDeath.SetActive(State == GameManager.GameState.death);
    }


    // Key is the key used to interact with the object
    // Message is a box text in the middle of the screen
    // InteractMessage is a box text below the Key
    public void SetInteractDisplay(string Key, string Message, string InteractMessage)
    {

        if (Key != null)
        {

            ActionText.SetActive(true);
            _ActionText = ActionText.GetComponent<TextMeshProUGUI>();
            _ActionText.text = Key;
        }
        if(Message != null) 
        {
            
            Text.SetActive(true);
            _Text  = Text.GetComponent<TextMeshProUGUI>();
            _Text.text = Message;
        }
        if(InteractMessage != null)
        {
            ActionDisplay.SetActive(true);
            _InteractMessage = ActionDisplay.GetComponent<TextMeshProUGUI>();
            _InteractMessage.text = InteractMessage;    

        }

    }

    

    public void UpdateInteractDisplay() 
    {
        ActionText.SetActive(false);
        Text.SetActive(false);
        ActionDisplay.SetActive(false);
        
    }

    public void UpdateObjective(string text)
    {
        if (text != null)
        {
            objectiveText.text = text;
            Objective.SetActive(true);
            Objective.GetComponent<Animation>().Play("objectiveUpdate");
        }
    }

}
