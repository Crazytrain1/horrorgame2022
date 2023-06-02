using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class MainSpawnerLevel1 : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] List<GameObject> Spawns;

    void Awake()
    {
        GameManager.Instance.UpdateGameState(GameState.Playing);
        GameManager.SpawnAction += loadPlayer;
    }

    private void OnDestroy()
    {
        GameManager.SpawnAction -= loadPlayer;
    }
 

    private void loadPlayer()
    {
        Player.transform.position = Spawns[GameManager.Instance.levelSpawn.y].transform.position;

    } 


}
