using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quote : MonoBehaviour
{
    private void OnDisable()
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.Playing);
    }
}
