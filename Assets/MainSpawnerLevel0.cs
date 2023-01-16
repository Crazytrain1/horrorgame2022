using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class MainSpawnerLevel0 : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject parent;
    void Start()
    {
        parent = Instantiate(Player as GameObject);

        parent.transform.position = this.transform.position;
        GameManager.Instance.UpdateGameState(GameState.Playing);
    }


}
