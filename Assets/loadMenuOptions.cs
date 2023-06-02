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

        if (GameManager.Instance.lastSave.Length == 0)
        {
            GameManager.Instance.UpdateLevel(GameManager.Level.Level0);
        }
        else
        {
            bool exist = GameManager.Instance.loadLevelSaved();
            if (exist)
            {
                GameManager.Instance.UpdateLevel(GameManager.Instance.LevelToLoad);
            }
            else
            {
                GameManager.Instance.UpdateLevel(GameManager.Level.Level0);
            }
        }

    }
}
