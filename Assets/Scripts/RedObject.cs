using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class RedObject : MonoBehaviour
{
    public float TheDistance;
    public GameObject ActionDisplay;
    public GameObject ActionText;
    public GameObject Object;
    public StarterAssets.StarterAssetsInputs starterAssets;
    // Start is called before the first frame update

    private void Awake()
    {
        starterAssets = new StarterAssets.StarterAssetsInputs();
    }
  
    private void Update()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;
        if (TheDistance <= 3)
        {
            ActionDisplay.SetActive(true);
            ActionText.SetActive(true);
            if (starterAssets.jump)
            {
                Debug.Log("interaction with object");
            }
        }
    }
    private void OnMouseOver()
    {
       
        
    }
}
