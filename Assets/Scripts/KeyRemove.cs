using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyRemove : MonoBehaviour
{
    [SerializeField] InteractDisplay _InteractDisplay;
    private void OnDestroy()
    {
        _InteractDisplay.UpdateInteractDisplay();
        _InteractDisplay.RemoveObjective("I must find the keys");
    }
}
