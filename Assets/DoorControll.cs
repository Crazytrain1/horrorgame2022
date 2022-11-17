
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControll : MonoBehaviour
{
    [SerializeField] float TheDistance;
    [SerializeField] GameObject doorframe;
    [SerializeField] GameObject ActionDisplay;
    [SerializeField] GameObject ActionText;
    [SerializeField] bool doorOpen;
    public Transform target;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
        TheDistance = PlayerCasting.DistanceFromTarget;
    }

    void OnMouseOver()
    {
        if(TheDistance <=3  )
        {
            
            ActionDisplay.SetActive(true);
            ActionText.SetActive(true); 
        }
        else
        {
            ActionDisplay.SetActive(false);
            ActionText.SetActive(false);

        }
        if(Input.GetKeyDown("e")&&TheDistance <=3&& !doorOpen)
        {
            Debug.Log("opening door");
            doorframe.GetComponent<Animation>().Play("DoorOpen");
            ActionDisplay.SetActive(false);
            ActionText.SetActive(false);
            StartCoroutine(DelayOpen());
            

        }
        else if (Input.GetKeyDown("e") && TheDistance <= 3 && doorOpen)
        {
            Debug.Log("closing door");
            doorframe.GetComponent<Animation>().Play("DoorClose");
            ActionDisplay.SetActive(false);
            ActionText.SetActive(false);            
            StartCoroutine(DelayClose());
        }

    }

     IEnumerator DelayOpen()
    {
        yield return new WaitForSeconds(1f);
        doorOpen = true;
    }
     IEnumerator DelayClose()
    {
        yield return new WaitForSeconds(1f);
        doorOpen = false;
    }

    void OnMouseExit()
    {
        ActionText.SetActive(false);
        ActionDisplay.SetActive(false);
    }
    
    
}
