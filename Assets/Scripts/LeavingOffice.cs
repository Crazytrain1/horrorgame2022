using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeavingOffice : MonoBehaviour
{
    [SerializeField] InteractDisplay _InteractDisplay;

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _InteractDisplay.RemoveObjective("I need to leave the office");
            _InteractDisplay.UpdateObjective("I must grab the flashlight before going down", 0);
            Destroy(gameObject);
        }

    }
}
