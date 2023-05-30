using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPrincipal : MonoBehaviour
{

    [SerializeField] enum Level { MainMenu, Level0, Level1, Level2, Level3, Level4 };
    [SerializeField] Level NextLevel;
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject loadMenu;
    [SerializeField] TextMeshProUGUI startButtonText;
    [SerializeField] TextMeshProUGUI loadButtonText;
    [SerializeField] Button loadButton;

    public void Start()
    {
        if (GameManager.Instance.lastSave.Length == 0)
        {
            Debug.Log(GameManager.Instance.lastSave);
            Debug.Log("null");
            startButtonText.SetText("New Game");
            loadButtonText.color = Color.red;
            loadButton.interactable = false;


        }
        else
        {
            Debug.Log(GameManager.Instance.lastSave);
            startButtonText.text.Equals("Continue");
            loadButton.interactable = true;
        }
    }
    public void OnQuitButton()
    {
        Application.Quit();
        
    }
    public void OnOptionsButton()
    {
        options.SetActive(true);
    }
    public void OnStartButton()
    {
        GameManager.Instance.UpdateLevel(GameManager.Level.Level0);
    }
    public void OnLoadButton()
    {
        loadMenu.SetActive(true);
    }
}
