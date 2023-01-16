using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class MainSpawnerLevel0 : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject parent;

    private void Awake()
    {
        parent = Instantiate(Player as GameObject);
    }


    void Start()
    {
        

        parent.transform.position = this.transform.position;
        //Debug.Log("position spawner");
        //Debug.Log(transform.position);
        //Debug.Log("position player");
        //Debug.Log(parent.transform.position);
        GameManager.Instance.UpdateGameState(GameState.Playing);
    }


}
