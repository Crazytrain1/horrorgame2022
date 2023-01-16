using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{

    public void OnReume()
    {
        GameManager.Instance.UpdateGameState(GameManager.Instance.PreviousState);
    }
    public void OnMainMenu()
    {
        GameManager.Instance.UpdateLevel(GameManager.Level.MainMenu);
    }

}
