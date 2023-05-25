using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{

    [SerializeField] GameObject options;

    public void OnResume()
    {
        GameManager.Instance.UpdateGameState(GameManager.Instance.PreviousState);
    }
    public void OnMainMenu()
    {
        GameManager.Instance.UpdateLevel(GameManager.Level.MainMenu);
    }

    public void OnOptions()
    {
        Debug.Log("menu d'options");
        options.SetActive(true);

    }

}
