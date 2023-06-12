using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using Cinemachine;
using System;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _loaderCanvas;
    [SerializeField] private Image _progressBar;


    [SerializeField] InventoryItemData FlashLight;
    [SerializeField] InventoryItemData KeyLevel0;

    private float _target;


    public static GameManager Instance;

    public Level LevelToLoad;
    public Level NextLevel;

    public GameState State;
    public GameState PreviousState;
    public GameState StartingState;
    public string lastSave = "";

    public static event Action<GameState> StateChanged;
    public static event Action<Level> LevelChanged;
    public static event Action SpawnAction;

    private IDataService DataService = new JsonDataService();
    private bool EncryptionEnabled;

    private List<String[]> inventoryList = new List<string[]>();
    public Vector2Int levelSpawn;
    

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


        //need to handle if there is no file
        loadLastSave();

    }
    private void Update()
    {
        if (_progressBar != null)
        {
            _progressBar.fillAmount = Mathf.MoveTowards(_progressBar.fillAmount, _target, 3 * Time.deltaTime);
        }
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
        Debug.Log(State.ToString() + "out of update game state");
        StateChanged?.Invoke(newState);
    }

    public  void UpdateLevel(Level newLevel)
    {
        _target = 0;
        _progressBar.fillAmount = 0;
        NextLevel = newLevel;
        switch (newLevel)
        {
            case Level.MainMenu:
                LoadScene("MenuPrincipal");
                StartingState = GameState.interacting;
                break;
            case Level.Level0:               
                LoadScene("Level_00");
                //potential lock cursor fix
                StartingState = GameState.cinematic;
                
                break;
            case Level.Level1:
                LoadScene("Level_01");
                //potential lock cursor fix
                StartingState = GameState.Playing;

                break;
            case Level.Level2:
                LoadScene("Level_02");
                //potential lock cursor fix
                StartingState = GameState.Playing;
                break;
            case Level.Level3:
                LoadScene("Level_03");
                //potential lock cursor fix
                StartingState = GameState.Playing;
                break;
            case Level.Level4:
                LoadScene("Level_04");
                //potential lock cursor fix
                StartingState = GameState.Playing;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newLevel), newLevel, null);
        }
        //Debug.Log(NextLevel.ToString());
        LevelChanged?.Invoke(newLevel);
        

    }

    private async void LoadScene(string sceneName)
    {
        
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;
        _loaderCanvas.SetActive(true);
        do
        {
            await Task.Delay(100);
            _target = scene.progress;
            Debug.Log("target");
            Debug.Log(_target);
            Debug.Log("progress bar");
            Debug.Log(_progressBar);
        }
        while (scene.progress < 0.9f);
        _target = 1;
        await Task.Delay(1000);
        scene.allowSceneActivation = true;
        _loaderCanvas.SetActive(false);
        loadLevelSpawn();
        UpdateGameState(StartingState);
        
    }

    public void loadLastSave()
    {

        lastSave = DataService.LoadData<String>("/lastSave.json", EncryptionEnabled);
    }
    public void saveLastSave()
    {
        if (DataService.SaveData("/lastSave.json", lastSave, EncryptionEnabled))
        {
            Debug.Log("Sheeeeesh");
        }
        else
        {
            Debug.Log("Could not save file!");
        }
    }

    public void loadInventory()
    {
        if (DataService.pathExist("/inventory" + lastSave + ".json"))
        {
            inventoryList = DataService.LoadData<List<String[]>>("/inventory" + lastSave + ".json", EncryptionEnabled);
            for (int i = 0; i < inventoryList.Count; i++)
            {
                if (inventoryList[i].First() == FlashLight.id)
                {
                    for (int j = 0; j < Int32.Parse(inventoryList[i].Last()); j++)
                    {

                        InventorySystem.current.Add(FlashLight);
                    }
                }
                if (inventoryList[i].First() == KeyLevel0.id)
                {
                    for (int j = 0; j < Int32.Parse(inventoryList[j].Last()); j++)
                    {
                        InventorySystem.current.Add(KeyLevel0);
                    }
                }

            }

            InventorySystem.current.InventoryChangedEvent();
        }
        else
        {
            Debug.Log("empty inventory");
        }
    }

    public void loadLevelSpawn()
    {

        SpawnAction?.Invoke();
        
    }

    public bool loadLevelSaved()
    {
        if(DataService.pathExist("/levelSpawn" + lastSave + ".json"))
        {
       
            levelSpawn = DataService.LoadData<Vector2Int>("/levelSpawn" + lastSave + ".json", EncryptionEnabled);
            if(levelSpawn.x == 0)
            {
                LevelToLoad = Level.Level0;
            }
            if (levelSpawn.x == 1)
            {
                LevelToLoad = Level.Level1;
            }
            if (levelSpawn.x == 2)
            {
                LevelToLoad = Level.Level2;
            }
            if (levelSpawn.x == 3)
            {
                LevelToLoad = Level.Level3;
            }
            if (levelSpawn.x == 4)
            {
                LevelToLoad = Level.Level4;
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public void saveLevel(int level, int spawn)
    {
       
        levelSpawn.x = level;
        levelSpawn.y = spawn;

        //save to file in json format
        if (DataService.SaveData("/levelSpawn" + lastSave + ".json", levelSpawn, EncryptionEnabled))
        {
            Debug.Log("Sheeeeesh");
        }
        else
        {
            Debug.Log("Could not save file!");
        }
    }

    public void saveInventory()
    {
        
        inventoryList.Clear();

        foreach (var i in InventorySystem.current.inventory)
        {
            
            inventoryList.Add(new string[]{ i.data.id, i.stackSize.ToString() });
           
        }
        if (DataService.SaveData("/inventory"+ lastSave + ".json", inventoryList, EncryptionEnabled))
        {
            Debug.Log("Sheeeeesh");
        }
        else
        {
            Debug.Log("Could not save file!");
        }
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
        MainMenu,
        Level0, 
        Level1, 
        Level2,
        Level3,
        Level4
    }


}
