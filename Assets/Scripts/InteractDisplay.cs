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
    private TextMeshProUGUI _Text;
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
    }

    public void SetInteractDisplay(bool Key, string Message, string InteractMessage)
    {
        if (Key)
        {

            ActionText.SetActive(true);
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

}
