using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorNextLevel1 : MonoBehaviour
{

    [SerializeField] float TheDistance;
    [SerializeField] GameObject InteractDisplayObject;
    [SerializeField] GameObject demoEnd;
    private int _DistanceMax = 2;
    private bool _open;
    private InteractDisplay _InteractDisplay;
    // Start is called before the first frame update
    void Start()
    {
        _open = false;
        if (InteractDisplayObject != null)
        {
            _InteractDisplay = InteractDisplayObject.GetComponent<InteractDisplay>();
            _InteractDisplay.UpdateInteractDisplay();
        }
    }

    // Update is called once per frame
    void Update()
    {

        TheDistance = PlayerCasting.DistanceFromTarget;

    
    }

    private void OnMouseOver()
    {
        if (TheDistance <= _DistanceMax)
        {

            _InteractDisplay.SetInteractDisplay("[E]", null, "open door");
            

        }
        else
        {
            _InteractDisplay.UpdateInteractDisplay();

        }
        if (Input.GetKeyDown("e") && TheDistance <= _DistanceMax && !_open)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.interacting);

            demoEnd.SetActive(true);
            _open = true;
            
            

        }



    }

    void OnMouseExit()
    {
        _InteractDisplay.UpdateInteractDisplay();
    }
}
