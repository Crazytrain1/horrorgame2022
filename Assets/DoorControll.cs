
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControll : MonoBehaviour
{
    [SerializeField] float TheDistance;
    [SerializeField] GameObject doorframe;  
    [SerializeField] GameObject InteractDisplayObject;
    [SerializeField] bool _doorlocked;
    private bool _doorOpen;
    private bool Interacting;

    private InteractDisplay _InteractDisplay;
    private int _DistanceMax = 2;
    // Start is called before the first frame update
    void Start()
    {
        _InteractDisplay = InteractDisplayObject.GetComponent<InteractDisplay>();
        _InteractDisplay.UpdateInteractDisplay();

    }

    // Update is called once per frame
    void Update()
    {
        
        TheDistance = PlayerCasting.DistanceFromTarget;
    }

    void OnMouseOver()
    {
        if(TheDistance <=_DistanceMax && !Interacting )
        {

            _InteractDisplay.SetInteractDisplay(true, null, "Press to Interact");
            
            
        }
        else
        {
            _InteractDisplay.UpdateInteractDisplay();

        }
        if(Input.GetKeyDown("e")&&TheDistance <= _DistanceMax && !_doorOpen)
        {
            if (!_doorlocked)
            {
                doorframe.GetComponent<Animation>().Play("DoorOpen");
                _InteractDisplay.UpdateInteractDisplay();
                StartCoroutine(DelayOpen());
            }
            else
            {
                //add jamming sound
            }

        }
        else if (Input.GetKeyDown("e") && TheDistance <= _DistanceMax && _doorOpen)
        {
            doorframe.GetComponent<Animation>().Play("DoorClose");
            _InteractDisplay.UpdateInteractDisplay();
            StartCoroutine(DelayClose());
        }

    }
    public void SetLock(bool locked)
    {
        _doorlocked= locked;
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
