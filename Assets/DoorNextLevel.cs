using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DoorNextLevel : MonoBehaviour
{

    [SerializeField] float TheDistance;
    [SerializeField] GameObject InteractDisplayObject;

    private InteractDisplay _InteractDisplay;
    private int _DistanceMax = 2;
    private IDataService DataService = new JsonDataService();
    private InventorySystem _InventorySystem;
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
           
            GameManager.Instance.UpdateLevel(GameManager.Level.Level1);
            _InteractDisplay.UpdateInteractDisplay();
        }

    }

    public void SerializeJson()
    {
        if (DataService.SaveData("/inventory.json", _InventorySystem, EncryptionEnabled))
        {
            Debug.Log("Sheeeeesh");
        }
        else
        {
            Debug.Log("Could not save file!");
        }

    }

    void OnMouseExit()
    {
        _InteractDisplay.UpdateInteractDisplay();
    }
}
