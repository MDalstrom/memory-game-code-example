using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class LocalizationImage : LocalizationComponent
{
    [SerializeField] private Sprite _ruTranslation;
    [SerializeField] private Sprite _enTranslation;
    protected override void SetValue(bool isRU)
    {
        if (GetComponent<Image>() is var imageHandler && imageHandler != null)
            imageHandler.sprite = isRU ? _ruTranslation : _enTranslation;
    }
}
