using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightRemove : MonoBehaviour
{
    [SerializeField] InteractDisplay _InteractDisplay;
    private void OnDestroy()
    {
        _InteractDisplay.UpdateInteractDisplay();
        _InteractDisplay.RemoveObjective("I must grab the flashlight before going down");
        _InteractDisplay.UpdateObjective("I should go down", 0);
    }
}
