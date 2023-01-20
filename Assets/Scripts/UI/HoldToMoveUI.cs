using System;
using UnityEngine;
using DG.Tweening;

public class HoldToMoveUI : MonoBehaviour
{
    [SerializeField] private GameObject _finger;
    [SerializeField] private GameObject _text;

    public void StartAnimation()
    {
        float screeWidthQuarter = Screen.width / 4;
        _finger.transform.DOLocalMove(new Vector3(screeWidthQuarter, _finger.transform.localPosition.y, 0), 0.75f)
            .OnComplete(() =>
                _finger.transform.DOLocalMove(new Vector3(-screeWidthQuarter, _finger.transform.localPosition.y, 0), 1.5f)
                    .SetLoops(-1, LoopType.Yoyo));
        _text.transform.DOShakeScale(2f, 0.05f).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDisable()
    {
        _finger.transform.DOKill();
        _text.transform.DOKill();
    }
}
