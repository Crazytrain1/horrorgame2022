using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.FlowStateWidget;

public class LeavingOffice : MonoBehaviour
{
    [SerializeField] InteractDisplay _InteractDisplay;

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _InteractDisplay.RemoveObjective("leave the office");
            _InteractDisplay.UpdateObjective("Go to the catacombs", 0);
            Destroy(gameObject);
        }

    }
}
