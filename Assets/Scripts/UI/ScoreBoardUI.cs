using System;
using TMPro;
using UnityEngine;

public class ScoreBoardUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void Update()
    {
        _scoreText.text = Mathf.RoundToInt(GamePlayManager.Instance.Score).ToString();
    }
}