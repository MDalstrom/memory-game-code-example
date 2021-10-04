using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LocalizationComponent : MonoBehaviour
{
    protected abstract void SetValue(bool isRU);
    private void Start()
    {
        bool isRU = Application.systemLanguage == SystemLanguage.Russian;
        SetValue(isRU);
    }
}
