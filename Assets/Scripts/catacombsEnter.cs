using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catacombsEnter : MonoBehaviour
{
    [SerializeField] InteractDisplay _InteractDisplay;

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            _InteractDisplay.UpdateObjective("I must close the breaker, it's pricey to let the electricity open during the night", 0);
            Destroy(gameObject);
        }

    }
}
