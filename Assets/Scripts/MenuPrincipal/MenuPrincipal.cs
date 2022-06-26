using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuPrincipal : MonoBehaviour
{
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
        SceneManager.LoadScene(1);
    }
}
