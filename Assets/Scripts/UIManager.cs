using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private ScoreBoardUI _scoreBoard;
    [SerializeField] private HoldToMoveUI _holdToMove;
    [SerializeField] private PauseUI _pause;
    [SerializeField] private LoseGameUI _loseGame;

    private void Awake()
    {
        DisableAllUI();
    }

    public void UpdateUI(GameState state)
    {
        switch (state)
        {
            case GameState.WaitingInput:
                DisableAllUI();
                _holdToMove.gameObject.SetActive(true);
                _holdToMove.StartAnimation();
                break;
            case GameState.Paused:
                DisableAllUI();
                _pause.gameObject.SetActive(true);
                _pause.StartAnimation();
                break;
            case GameState.InGame:
                DisableAllUI();
                _scoreBoard.gameObject.SetActive(true);
                break;
            case GameState.LoseGame:
                DisableAllUI();
                _loseGame.gameObject.SetActive(true);
                _loseGame.PlayAnimation();
                break;
            case GameState.Restart:
                break;
        }
    }

    private void DisableAllUI()
    {
        _scoreBoard.gameObject.SetActive(false);
        _holdToMove.gameObject.SetActive(false);
        _pause.gameObject.SetActive(false);
        _loseGame.gameObject.SetActive(false); 
    }
    
}
