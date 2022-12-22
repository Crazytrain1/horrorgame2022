using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractDisplay : MonoBehaviour
{
    [SerializeField] GameObject ActionDisplay;
    [SerializeField] GameObject ActionText;
    [SerializeField] GameObject Text;

    public void SetInteractDisplay(bool Key, string Message, string InteractMessage)
    {
        if (Key)
        {

            ActionText.SetActive(true);
        }
        if(Message != null) 
        {
            
            Text.SetActive(true);
        }
        if(InteractMessage != null)
        {
            ActionDisplay.SetActive(true);
        }

    }

    public void UpdateInteractDisplay() 
    {
        ActionText.SetActive(false);
        Text.SetActive(false);
        ActionDisplay.SetActive(false);
        
    }

}
