
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControll : MonoBehaviour
{
    [SerializeField] float TheDistance;
    [SerializeField] GameObject doorframe;
    [SerializeField] enum doorSide { interieur, exterieur};
    [SerializeField] doorSide doorKnob;
    [SerializeField] float mouseSensitivity = 100f;
    [SerializeField] GameObject ActionDisplay;
    [SerializeField] GameObject ActionText;
    [SerializeField] bool doorOpen;
    float xRotation = 0f;
    public float direction = -1f;
    public float speed = 1f;
    public Transform target;
    
    // Start is called before the first frame update
    void Start()
    {

        
       /* if()
        {
            direction = 1f;
        }
        if(doorSide(interieur))
        {
            direction = -1f;
        }
        */
       
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
            doorOpen = true;

        }
        else if (Input.GetKeyDown("e") && TheDistance <= 3 && doorOpen)
        {
            Debug.Log("closing door");
            doorframe.GetComponent<Animation>().Play("DoorClose");
            ActionDisplay.SetActive(false);
            ActionText.SetActive(false);
            doorOpen = false;

        }

    }

    void OnMouseExit()
    {
        ActionText.SetActive(false);
        ActionDisplay.SetActive(false);
    }
    /*
    IEnumerator Open()
    {
        
        target poisition is cursor
        Vector3 targetDirection = target.position - door.transform.position;
        float singleStep = speed * Time.deltaTime;

        Vector3 newDirection = Vector3.RotateTowards(door.transform.forward, targetDirection, singleStep, 0.0f);
        
        Debug.DrawRay(transform.position, newDirection, Color.red);
        
        door.transform.rotation = Quaternion.LookRotation(newDirection);
        Debug.Log("close enough");
        
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        

        door.transform.Rotate(0, mouseX * direction, 0, Space.Self);
        yield return null;
        
        
    }
    */
    
}
