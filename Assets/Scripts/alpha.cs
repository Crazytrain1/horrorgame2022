using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class alpha : MonoBehaviour
{

    [SerializeField] int alpha1;
    [SerializeField] TextMeshProUGUI text;

    // Update is called once per frame
    void Update()
    {
        text.faceColor = new Color32(255, 255, 255, (byte)alpha1); 
    }
}
