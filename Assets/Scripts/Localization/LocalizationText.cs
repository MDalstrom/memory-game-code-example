using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class LocalizationText : LocalizationComponent
{
    [SerializeField] private string _ruTranslation;
    [SerializeField] private string _enTranslation;
    protected override void SetValue(bool isRU)
    {
        if (GetComponent<Text>() is var textHandler && textHandler != null)
            textHandler.text = isRU ? _ruTranslation : _enTranslation;
    }
}
