using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    [SerializeField] protected GameObject PlayerCharacter;
    [SerializeField] protected Collider PlayerCollider;
    [SerializeField] protected Collider interactableCollider;
    [SerializeField] protected Vector3 PlayerPosition;
    [SerializeField] protected int objectType = 1;
    [SerializeField] public static bool crouching;
    public GameObject realItem;
    private float originalSize;
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
        if(objectType == 3)
        {
            
            interactIcon.SetActive(false);
            interactText.SetActive(false);
            crouching = true;
            player._playerInput.actions.Disable();
            Debug.Log("test 1");
            player._playerInput.SwitchCurrentActionMap("Crawl");
            Debug.Log("test 2");
            PlayerCollider = PlayerCharacter.GetComponent<Collider>();
            Object3D = this.gameObject;
            PlayerCharacter.transform.rotation = Object3D.transform.rotation;
            
            StartCoroutine("SmallSpace");
            Debug.Log("test 3");
            crouching = true;
            
        }
        if (objectType == 4)
        {

            Debug.Log("souffrance et desespoir");

        }


        Debug.Log("Interaction");
    }
    public void InteractLeave()
    {
        BackgroundItem.SetActive(false);
        InteractCamera.SetActive(false);
        LeaveButton.SetActive(false);
        player._playerInput.actions.Enable();
        player._playerInput.SwitchCurrentActionMap("Player");

    }
    IEnumerator SmallSpace()
    {
        while (crouching == true)
        {
            PlayerPosition = PlayerCharacter.transform.position;
            if (Keyboard.current.dKey.isPressed)

            {
                float timeElapsed = 0f;
                float totalLerpTime = 2f;

                while (timeElapsed <= totalLerpTime)
                {
                    
                    
                    PlayerCharacter.transform.position = Vector3.Lerp(PlayerPosition, PlayerPosition + new Vector3(1, 0, 0),(timeElapsed/totalLerpTime));
                    timeElapsed += Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                }
                yield return new WaitForSeconds((float)0.25);
            }
            else if  (Keyboard.current.aKey.isPressed)

            {
                Debug.Log("gauche");
                PlayerCharacter.transform.position += -transform.right  ;
                yield return new WaitForSeconds(1);
            }

            yield return null; 
            
        }
        Debug.Log("pas dans la boucle");
        LeavingCrawl();
        yield  break;
    }

    public void LeavingCrawl()
    {
        PlayerCharacter.transform.position = Vector3.Lerp(PlayerPosition, PlayerPosition + new Vector3(10, 0, 0), (1));

        player._playerInput.actions.Enable();
        player._playerInput.SwitchCurrentActionMap("Player");
    }
}
