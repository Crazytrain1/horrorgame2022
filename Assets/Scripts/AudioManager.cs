using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource audioSource;    

    [SerializeField] AudioMixer mixer;

    public const string MUSIC_KEY = "musicVolume";
    public const string SFX_KEY = "sfxVolume";
    public const string MASTER_KEY = "masterVolume";
    private void Awake()
    {
        if(instance == null) { 
        instance = this; 
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        LoadVolume();
    }

    void LoadVolume()
    {
        float masterVolume = PlayerPrefs.GetFloat(MASTER_KEY, 1f);
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        float sfxVolume = PlayerPrefs.GetFloat(SFX_KEY, 1f);
        mixer.SetFloat(MenuOptions.MIXER_MUSIC, Mathf.Log10(musicVolume) * 20);
        mixer.SetFloat(MenuOptions.MIXER_MASTER, Mathf.Log10(masterVolume)*20);
        mixer.SetFloat(MenuOptions.MIXER_SFX, Mathf.Log10(sfxVolume) *20);
    }


}
