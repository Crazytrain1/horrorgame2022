using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class MainSpawnerLevel0 : MonoBehaviour
{
    [SerializeField] GameObject Player;

    void Awake()
    {
        

        Player.transform.position = this.transform.position;
        GameManager.Instance.UpdateGameState(GameState.Playing);
    }


}
