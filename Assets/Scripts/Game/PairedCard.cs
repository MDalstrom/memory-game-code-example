using System;
using UnityEngine;
using DG.Tweening;

public class PairedCard : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private SpriteRenderer _backside;
    [SerializeField] private SpriteRenderer _iconHolder;
    [Header("Timings")]
    [SerializeField] private float _rotatingTime;
    [SerializeField] private float _disposingTime;
    public string ID { get; set; }
    public bool Disposing { get; set; }

    public void SetIcon(Sprite icon)
    {
        _iconHolder.sprite = icon;
        _backside.sortingOrder = 3;
    }
    public void Show(Action onComplete)
    {
        AnimateRotationY(180, onComplete);
    }
    public void Hide(Action onComplete)
    {
        AnimateRotationY(360, onComplete);
    }
    public void Dispose(Action onComplete)
    {
        transform.DOScale(Vector3.zero, _disposingTime).SetEase(Ease.OutSine).OnComplete(() => {
            Destroy(gameObject);
            onComplete?.Invoke();
        });
    }
    private void SwitchSides()
    {
        _backside.sortingOrder = 3 - _backside.sortingOrder;
    }
    private void AnimateRotationY(float end, Action onComplete)
    {
        float startY = transform.localEulerAngles.y;
        transform.DOLocalRotate(new Vector3(0, startY + (end - startY) / 2f, 0), _rotatingTime / 2f)
            .SetEase(Ease.InSine)
            .OnComplete(() => {
                SwitchSides();
                transform.DOLocalRotate(new Vector3(0, end, 0), _rotatingTime / 2f)
                .SetEase(Ease.OutSine)
                .OnComplete(() => onComplete?.Invoke());
            });
    }
    public override string ToString()
    {
        return ID;
    }
}