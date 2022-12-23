using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using Cinemachine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    [SerializeField] GameObject Spawner;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject parent;
    [SerializeField] Transform parentT;
    [SerializeField] enum Level {MainMenu,Level0,Level1,Level2,Level3, Level4};
    [SerializeField] Level NextLevel;

    public GameState State;

    public static event Action<GameState> StateChanged;
    private void Awake()
    {
        Instance = this;
    }

    void Start()

    {
        UpdateGameState(GameState.Playing);

        
        parent = Instantiate(Player as GameObject);
        
        parent.transform.position = Spawner.transform.position;

        
        parentT = parent.transform;
      
        
        
    }
    public void UpdateGameState(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.Playing:
                break;
            case GameState.cinematic:
                break;
            case GameState.death:
                break;
            case GameState.Pausing:
                break;
            case GameState.interacting:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        StateChanged?.Invoke(newState);
    }
    public enum GameState
    {
        Playing,
        Pausing,
        interacting,
        death,
        cinematic

        
    }

}
