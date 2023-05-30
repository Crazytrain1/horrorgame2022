using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadMenuOptions : MonoBehaviour
{


    [SerializeField] private GameObject LoadMenu;


    public void GoBack()
    {
        LoadMenu.SetActive(false);
    }

    public void OnLoadButtonSave(string saveFile) 
    {

        GameManager.Instance.lastSave = (saveFile);


    //get load level and call the game manager funciton with it 

    }
}
