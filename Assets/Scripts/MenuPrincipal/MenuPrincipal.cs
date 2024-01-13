using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPrincipal : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;

    public const string MIXER_MASTER = "MasterVolume";
    public const string MIXER_MUSIC = "MusicVolume";
    public const string MIXER_SFX = "SFXVolume";
    

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
            GameManager.Instance.lastSave = "saveFile1";
            GameManager.Instance.saveLastSave();


        }
        else
        {
            Debug.Log(GameManager.Instance.lastSave);
            startButtonText.text.Equals("Continue");
            loadButton.interactable = true;
        }

        mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(PlayerPrefs.GetFloat(AudioManager.MUSIC_KEY, 1f)) * 20);
        mixer.SetFloat(MIXER_MASTER, Mathf.Log10(PlayerPrefs.GetFloat(AudioManager.MASTER_KEY, 1f)) * 20);
        mixer.SetFloat(MIXER_SFX, Mathf.Log10(PlayerPrefs.GetFloat(AudioManager.SFX_KEY, 1f)) * 20);
       

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

        //need to change for new way do detect the existence of a file
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
    public void OnLoadButton()
    {
        loadMenu.SetActive(true);
    }
}
