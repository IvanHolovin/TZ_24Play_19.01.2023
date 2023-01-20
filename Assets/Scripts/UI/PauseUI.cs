using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private GameObject _finger;
    [SerializeField] private GameObject _text;
    [SerializeField] private Button _menuButton;


    private void Awake()
    {
        _menuButton.onClick.AddListener(() => Debug.Log("Go to Main Menu"));
    }


    public void StartAnimation()
    {
        float screeWidthQuarter = Screen.width / 4;
        _finger.transform.DOLocalMove(new Vector3(screeWidthQuarter, _finger.transform.localPosition.y, 0), 0.75f).SetEase(Ease.Linear)
            .OnComplete(() =>
                _finger.transform.DOLocalMove(new Vector3(-screeWidthQuarter, _finger.transform.localPosition.y, 0), 1.5f)
                    .SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo));
        _text.transform.DOShakeScale(2f, 0.05f).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDisable()
    {
        _finger.transform.DOKill();
        _text.transform.DOKill();
    }  
}
