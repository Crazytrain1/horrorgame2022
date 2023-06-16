
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DoorControll : MonoBehaviour
{
    [SerializeField] float TheDistance;
    [SerializeField] GameObject doorframe;  
    [SerializeField] GameObject InteractDisplayObject;
    [SerializeField] bool _doorlocked;
    public bool notclockIn;
    [SerializeField] AudioSource _doorOpenSound;
    [SerializeField] AudioSource _doorCloseSound;
    private bool _doorOpen;
    private bool Interacting;

    public InventoryItemData referenceItem;

    private InteractDisplay _InteractDisplay;
    private int _DistanceMax = 2;
    
    void Start()
    {
        if (InteractDisplayObject != null)
        {
            _InteractDisplay = InteractDisplayObject.GetComponent<InteractDisplay>();
            _InteractDisplay.UpdateInteractDisplay();
        }

    }

    
    void Update()
    {
        
        TheDistance = PlayerCasting.DistanceFromTarget;
    }

    void OnMouseOver()
    {
        if(TheDistance <=_DistanceMax && !Interacting )
        {
            if (_doorOpen)
            {
                _InteractDisplay.SetInteractDisplay("[E]", null, "close door");
            }
            else
            {
                _InteractDisplay.SetInteractDisplay("[E]", null, "open door");
            }
            
        }
        else
        {
            _InteractDisplay.UpdateInteractDisplay();

        }
        if(Input.GetKeyDown("e") && TheDistance <= _DistanceMax && !_doorOpen && !Interacting)
        {
            if (!_doorlocked)
            {
                if (!notclockIn) 
                {
                
                    doorframe.GetComponent<Animation>().Play("DoorOpen");
                _InteractDisplay.UpdateInteractDisplay();
                _doorOpenSound.Play();
                StartCoroutine(DelayOpen());
                }
                else
                {
                    _InteractDisplay.SetInteractDisplay("[E]", "you need to clock in", "open door");
                }
               
            }
            else
            {
                if (InventorySystem.current.Get(referenceItem) == null)
                {
                    _InteractDisplay.SetInteractDisplay("[E]", "you need the keys", "open door");
                }
                else
                {
                    
                    SetLock(false);
                    InventorySystem.current.Remove(referenceItem);
                }
            }

        }
        else if (Input.GetKeyDown("e") && TheDistance <= _DistanceMax && _doorOpen && !Interacting)
        {
            doorframe.GetComponent<Animation>().Play("DoorClose");
            _doorCloseSound.Play();
            _InteractDisplay.UpdateInteractDisplay();         
            StartCoroutine(DelayClose());
        }

    }
    public void SetLock(bool locked)
    {
        _doorlocked= locked;
    }
    public void ClockIn()
    {
        notclockIn= false;
    }

     IEnumerator DelayOpen()
    {
        Interacting= true;
        yield return new WaitForSeconds(1f);
        _doorOpen = true;
        Interacting= false;
    }
     IEnumerator DelayClose()
    {
        Interacting = true;
        yield return new WaitForSeconds(1f);
        _doorOpen = false;
        Interacting= false;
    }

    void OnMouseExit()
    {
        _InteractDisplay.UpdateInteractDisplay();
    }
    
    
}
