using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    // base class for interactable objects

    public ThirdPersonController player;
    [SerializeField] protected GameObject interactIcon;
    [SerializeField] protected GameObject interactText;
    [SerializeField] protected GameObject BackgroundItem;
    [SerializeField] protected GameObject LeaveButton;
    [SerializeField] protected GameObject Object3D;
    [SerializeField] protected GameObject InteractCamera;
    protected Collider interactableCollider;
    [SerializeField] protected int objectType = 1;
    public GameObject realItem;
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

            if (player.canInteract == false)
            {
                player._input.Interact = false;
                player.canInteract = false;
            }
            else
            {
             
                player.canInteract = true;
            }
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
        if (objectType == 1)
        {
            realItem = this.gameObject;
            realItem.TryGetComponent<ItemObject>(out ItemObject Item);
            Item.OnPickupItem();
            interactIcon.SetActive(false);
            interactText.SetActive(false);
        }

        if (objectType == 2)
        {

            BackgroundItem.SetActive(true);
            interactIcon.SetActive(false);
            interactText.SetActive(false);
            InteractCamera.SetActive(true);
            LeaveButton.SetActive(true);
            player._playerInput.actions.Disable();
            player._playerInput.SwitchCurrentActionMap("Item Viewing");
            Object3D = this.gameObject;
          

        }


        Debug.Log("Interaction");
    }
    public void InteractLeave()
    {
        BackgroundItem.SetActive(false);
        InteractCamera.SetActive(false);
        LeaveButton.SetActive(false);
        player._playerInput.actions.Enable();
    }
}
