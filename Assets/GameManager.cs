using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using Cinemachine;
using System;
using UnityEngine.InputSystem.LowLevel;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    [SerializeField] GameObject Spawner;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject parent;
    [SerializeField] Transform parentT;

    public Level CurrentLevel;
    public Level NextLevel;

    public GameState State;
    public GameState PreviousState;

    public static event Action<GameState> StateChanged;
    public static event Action<Level> LevelChanged;
    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        parent = Instantiate(Player as GameObject);

        parent.transform.position = Spawner.transform.position;


        parentT = parent.transform;
    }

    void Start()

    {
        
        UpdateGameState(GameState.Playing);
        
    }
    public void UpdateGameState(GameState newState)
    {
        PreviousState = State;
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
        Debug.Log(State.ToString());
        StateChanged?.Invoke(newState);
    }

    public void UpdateLevel(Level newLevel)
    {
        NextLevel = newLevel;
        switch (newLevel)
        {
            case Level.Level0:
                SceneManager.LoadScene("Level_00");
                break;
            case Level.Level1:
                SceneManager.LoadScene("Level_01");
                break;
            case Level.Level2:
                break;
            case Level.Level3:
                break;
            case Level.Level4:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newLevel), newLevel, null);
        }
        Debug.Log(NextLevel.ToString());
        LevelChanged?.Invoke(newLevel);
    }
    public enum GameState
    {
        Playing,
        Pausing,
        interacting,
        death,
        cinematic


    }
    public enum Level
    {
        Level0, 
        Level1, 
        Level2, Level3,
        Level4
    }


}
