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

    private void Start()
    {
        GameManager.Instance.loadLevelSpawn();
    }

    private void OnDestroy()
    {
        GameManager.SpawnAction -= loadPlayer;
    }
 

    private void loadPlayer()
    {

        Debug.Log("player loaded sikeeeeeee");
        Player.transform.position = Spawns[GameManager.Instance.levelSpawn.y].transform.position;
        GameManager.Instance.loadInventory();
    } 


}
