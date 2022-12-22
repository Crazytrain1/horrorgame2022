
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControll : MonoBehaviour
{
    [SerializeField] float TheDistance;
    [SerializeField] GameObject doorframe;  
    [SerializeField] GameObject InteractDisplayObject;
    [SerializeField] bool doorOpen;
    private bool Interacting;
    public Transform target;
    private InteractDisplay _InteractDisplay;
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
        if(TheDistance <=3 != Interacting )
        {

            _InteractDisplay.SetInteractDisplay(true, null, "Press to Interact");
            
            
        }
        else
        {
            _InteractDisplay.UpdateInteractDisplay();

        }
        if(Input.GetKeyDown("e")&&TheDistance <=3&& !doorOpen)
        {
            
            doorframe.GetComponent<Animation>().Play("DoorOpen");
            _InteractDisplay.UpdateInteractDisplay();
            StartCoroutine(DelayOpen());
            

        }
        else if (Input.GetKeyDown("e") && TheDistance <= 3 && doorOpen)
        {
            doorframe.GetComponent<Animation>().Play("DoorClose");
            _InteractDisplay.UpdateInteractDisplay();
            StartCoroutine(DelayClose());
        }

    }

     IEnumerator DelayOpen()
    {
        Interacting= true;
        yield return new WaitForSeconds(1f);
        doorOpen = true;
        Interacting= false;
    }
     IEnumerator DelayClose()
    {
        Interacting = true;
        yield return new WaitForSeconds(1f);
        doorOpen = false;
        Interacting= false;
    }

    void OnMouseExit()
    {
        _InteractDisplay.UpdateInteractDisplay();
    }
    
    
}
