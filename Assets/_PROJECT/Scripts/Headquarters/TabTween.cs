using UnityEngine;
using DG.Tweening;
using System;

public class TabTween : MonoBehaviour
{

    [SerializeField] private float panelEnterDuration;
    [SerializeField] private float panelExitDuration;
    [SerializeField] private Vector2 endLocation;

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Tween(Action callback)
    {
        _rectTransform.DOAnchorPos(Vector2.zero, panelEnterDuration, false).OnComplete(()=> { callback?.Invoke(); });
    }

    public void ResetTween()
    {
        _rectTransform.DOAnchorPos(endLocation, panelExitDuration).OnComplete(()=> {    });
    }

}
