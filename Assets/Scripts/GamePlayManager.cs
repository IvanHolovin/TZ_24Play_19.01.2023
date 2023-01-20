using System;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private TrackSpawner _trackSpawner;
    [SerializeField] private BlockStackManager _playerBlockStack;
    
    private static GamePlayManager _instance;
    private int _score;
    
    public static GamePlayManager Instance => _instance;


    public int Score => _score;
    

    public GameState State { get; private set; }
        
    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        GameStateUpdater(GameState.WaitingInput);
        _uiManager.UpdateUI(GameState.WaitingInput);
        _trackSpawner.InitializeTracks();
    }

    private void Update()
    {
        if (_playerBlockStack.gameObject.transform.position.z > _trackSpawner.SpawnNewTrackPoint())
        {
            _trackSpawner.SpawnNewPart();
        }
    }


    public void GameStateUpdater(GameState newState)
    {
        if (newState != State)
        {
            State = newState;
        
            switch (newState)
            {
                case GameState.WaitingInput:
                    break;
                case GameState.InGame:
                    break;
                case GameState.Paused:
                    break;
                case GameState.LoseGame:
                    Debug.Log("loose");
                    Debug.Log("Set Player pref new score");
                    break;
                case GameState.Restart:
                    //restart
                    break;
            } 
            _uiManager.UpdateUI(newState);
        }
    }

    public void AddScore(float value)
    {
        _score += Mathf.RoundToInt(value);
    }
    
    
}

public enum GameState
{
    WaitingInput,
    Paused,
    InGame,
    LoseGame,
    Restart
}
