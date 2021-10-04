using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickHandler : MonoBehaviour
{
    public UnityEvent Pressed;
    private void OnMouseDown()
    {
        Pressed?.Invoke();
    }
}
