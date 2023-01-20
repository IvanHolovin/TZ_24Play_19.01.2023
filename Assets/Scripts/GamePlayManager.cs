using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    private static GamePlayManager _instance;
    private int _score;
    
    public static GamePlayManager Instance => _instance;


    public int Score => _score;
    

    public GameState State { get; private set; }
        
    private void Awake()
    {
        _instance = this;
        GameStateUpdater(GameState.WaitingInput);
    }


    public void GameStateUpdater(GameState newState)
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
                break;
        }
    }
    
    
    
}

public enum GameState
{
    WaitingInput,
    Paused,
    InGame,
    LoseGame,
}
