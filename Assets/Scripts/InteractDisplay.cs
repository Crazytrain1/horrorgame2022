using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractDisplay : MonoBehaviour
{
    [SerializeField] GameObject ActionDisplay;
    [SerializeField] GameObject ActionText;
    [SerializeField] GameObject Text;
    private TextMeshProUGUI _Text;
    private TextMeshProUGUI _InteractMessage;

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
