using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quote : MonoBehaviour
{
    [SerializeField] InteractDisplay Interactdisplay;
    private void Start()
    {
        
    }
    private void OnDisable()
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.Playing);
        Interactdisplay.UpdateObjective("Explore the office");
    }
}
