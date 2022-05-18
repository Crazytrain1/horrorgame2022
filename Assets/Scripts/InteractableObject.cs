using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    // base class for interactable objects
    
    protected ThirdPersonController player;
   [SerializeField] protected GameObject interactIcon;
   [SerializeField] protected GameObject interactText;
    protected Collider interactableCollider;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(player == null)
        {
            player = other.GetComponent<ThirdPersonController>();

        }
        if (player != null)
        {
            interactIcon.SetActive(true);
            interactText.SetActive(true);
            player.canInteract = true;
        }
    }
    protected virtual void OnTriggerStay(Collider other)
    {
        if (player != null)
        {
            if (player._input.Interact)
            {
                
                Interact(player);
                player._input.Interact = false;
            }
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if(player == null)
        {
            player = other.GetComponent<ThirdPersonController>();
        }
        if (player != null)
        {
            interactIcon.SetActive(false);
            interactText.SetActive(false);
            player.canInteract = false;
        }
    }
    protected virtual void Interact(ThirdPersonController player)
    {
        Debug.Log("Interaction");
    }
}
