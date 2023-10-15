using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewingUI : MonoBehaviour
{
    [SerializeField] GameObject cameraView;
  
    public void OnViewClose()
    {
        
        cameraView.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
