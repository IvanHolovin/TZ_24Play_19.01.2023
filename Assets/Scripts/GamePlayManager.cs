using System;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private TrackSpawner _trackSpawner;
    [SerializeField] private BlockStackManager _playerBlockStack;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private int _speed;
    
    private static GamePlayManager _instance;
    private float _score;
    
    public static GamePlayManager Instance => _instance;


    public float Score => _score;
    public int Speed => _speed;
    

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

        if (State == GameState.InGame)
        {
            _score += Time.deltaTime * _speed;
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
                    Debug.Log("Set Player pref new score");
                    break;
                case GameState.Restart:
                    _playerMovement.RestartPlayer();
                    _playerBlockStack.RestartBlocks();
                    _trackSpawner.RestartTracks();
                    _score = 0;
                    GameStateUpdater(GameState.WaitingInput);
                    break;
            }
            _uiManager.UpdateUI(newState);
        }
    }

    public void AddScore(float value)
    {
        //_score += value;
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
