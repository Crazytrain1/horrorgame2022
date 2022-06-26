using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuOptions : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject options;
    public void OnWindowMode()
        {
        Screen.fullScreen = true;
        }
    public void OnMaster()
    {

    }
    public void OnMusic()
    {

    }
    public void OnSFX()
    {

    }
    public void GoBack()
    {
        options.SetActive(false);
    }
}
