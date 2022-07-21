using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuPrincipal : MonoBehaviour
{

    [SerializeField] enum Level { MainMenu, Level0, Level1, Level2, Level3, Level4 };
    [SerializeField] Level NextLevel;
    [SerializeField] private GameObject options;
    public void OnQuitButton()
    {
        Application.Quit();
        Debug.Log("quitting the game, u pussy?");
    }
    public void OnOptionsButton()
    {
        options.SetActive(true);
    }
    public void OnStartButton()
    {
        SceneManager.LoadScene((int)NextLevel);
    }
}
