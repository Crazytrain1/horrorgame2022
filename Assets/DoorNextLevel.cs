using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DoorNextLevel : MonoBehaviour
{

    [SerializeField] float TheDistance;
    [SerializeField] GameObject InteractDisplayObject;
    private InventorySystem.InventoryItem InventoryItem;
    private InteractDisplay _InteractDisplay;
    private int _DistanceMax = 2;
    private IDataService DataService = new JsonDataService();
    private string test = "I have commited war crimes in Yougoslavia";
    private bool EncryptionEnabled;
    
    
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

    private void OnMouseOver()
    {
        if (TheDistance <= _DistanceMax)
        {

            _InteractDisplay.SetInteractDisplay("[E]", null, "enter Catacombs");


        }
        else
        {
            _InteractDisplay.UpdateInteractDisplay();

        }
        if (Input.GetKeyDown("e") && TheDistance <= _DistanceMax)
        {
            GameManager.Instance.saveInventory();
            GameManager.Instance.UpdateLevel(GameManager.Level.Level1);
            _InteractDisplay.UpdateInteractDisplay();
        }

    }


      

    

    void OnMouseExit()
    {
        _InteractDisplay.UpdateInteractDisplay();
    }
}
