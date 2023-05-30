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
        GameManager.Instance.saveLastSave();

        //need to change it so it takes the right level for the file save
        GameManager.Instance.UpdateLevel(GameManager.Level.Level0);

    }
}
