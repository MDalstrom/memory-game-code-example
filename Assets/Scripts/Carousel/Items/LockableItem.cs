using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class LockableItem : ICarouselItem
{
    [SerializeField] private Image _lock;
    [SerializeField] protected RawImage _imageHolder;
    protected bool _isLocked;
    public virtual void Lock()
    {
        _lock.enabled = true;
        _imageHolder.color = Color.grey;
        _isLocked = true;
    }
    public virtual void Unlock()
    {
        _lock.enabled = false;
        _imageHolder.color = Color.white;
        _isLocked = false;
    }
    public void Open()
    {
        if (_isLocked)
            OnLockedOpened();
        else
            OnUnlockedOpened();
    }
    protected abstract void OnLockedOpened();
    protected abstract void OnUnlockedOpened();
}
