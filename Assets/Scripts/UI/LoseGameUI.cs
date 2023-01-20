using System;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class LoseGameUI : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private Button _tryAgainButton;
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void Awake()
    {
        _tryAgainButton.onClick.AddListener(() => GamePlayManager.Instance.GameStateUpdater(GameState.Restart));
    }


    public void PlayAnimation()
    {
        _background.DOFade(60, 1.5f);
        _scoreText.text = "Score: " + GamePlayManager.Instance.Score;
    }

    private void OnDisable()
    {
       _background.color = new Color( 255,0,0,0);
       _background.DOKill();
    }
}
