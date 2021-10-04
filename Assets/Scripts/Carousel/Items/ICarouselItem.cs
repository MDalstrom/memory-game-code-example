using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ICarouselItem : MonoBehaviour
{
    public abstract void HandleValue(float value);
    public string CategoryName { get; set; }
}
