using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QTEslider : MonoBehaviour
{
    public GameObject minigame;
    public GameObject handle;
    public GameObject bar;
    public GameObject hardzone;
    public RectTransform rtstart;
    public Vector3 vectorLeft;
    public Vector3 vectorRight;
    [SerializeField] bool moveRight;
    [SerializeField] bool moveLeft;
    [SerializeField] bool interacting;
    [SerializeField] float Lefthard;
    [SerializeField] float Righthard;

    [SerializeField] int tryLeft = 3;




    // Start is called before the first frame update


    void Start()
    {
     
 
       
        rtstart = handle.GetComponent<RectTransform>();
        var barvar = bar.GetComponent<RectTransform>();;
        var hard = hardzone.GetComponent<RectTransform>();
        Lefthard = hard.rect.xMin + hard.position.x;
        Righthard = hard.rect.xMax + hard.position.x;
        float Left = barvar.rect.xMin + barvar.position.x;
        float Right = barvar.rect.xMax + barvar.position.x;
        vectorLeft = rtstart.transform.position;
        vectorRight = rtstart.transform.position;
        vectorLeft.x = Left;
        vectorRight.x = Right;
        rtstart.transform.position = vectorLeft;
    }

    // Update is called once per frame
    void Update()
    {

        var rt = handle.GetComponent<RectTransform>();
        var rtbar = bar.GetComponent<RectTransform>();


        //need to change for new input system
        /*if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E))
        {
            interacting = true;
            //when the player hit the right place
            if (Lefthard <= rt.position.x && rt.position.x <= Righthard)
            {
                Debug.Log("hit");
                minigame.SetActive(false);

            }
            //when the player miss
            else
            {
                Debug.Log("miss");
                AnotherTry();
            }
      
        }*/

        // decide when the bar should move right
        if ((rt.position.x <= rtstart.position.x) && !moveRight && !moveLeft && !interacting)
        {
            moveRight = true;




        }
        //Switch to moving left after reaching the right side
        if (rt.position.x >= vectorRight.x && !interacting)
        {
            moveLeft = true;
            moveRight = false;


        }
        // Switch to moving right after reaching the left side
        if (rt.position.x <= vectorLeft.x && !interacting)
        {
            moveLeft = false;
            moveRight = true;


        }
        // for moving right
        if (moveRight && !interacting)
        {
            //Debug.Log("moving right");
            rt.position = Vector3.MoveTowards(rt.position, vectorRight, 1000.0f * Time.deltaTime);

        }
        //for moving left
        if (moveLeft && !interacting)
        {
            //Debug.Log("movingleft");
            rt.position = Vector3.MoveTowards(rt.position, vectorLeft, 1000.0f * Time.deltaTime);
        }





    }
    // recall the function with parameters with default values to try again
    private void AnotherTry()
    {
        interacting = false;
        rtstart.transform.position = vectorLeft;
    }
}

