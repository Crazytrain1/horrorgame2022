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
        Interactdisplay.UpdateObjective("I should read the instruction Pierre left me in his office", 0);
    }
}
