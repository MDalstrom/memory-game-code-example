using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using System;

[RequireComponent(typeof(ScrollRect))]
public class Carousel : ScrollRect
{
    private const float Spacing = 150f;
    private List<ICarouselItem> _items;
    private float _actualWidth;
    private float _delta;
    private float _height;
    public void Initialize()
    {
        _items = content.GetComponentsInChildren<ICarouselItem>().ToList();
        _height = viewport.rect.height;
        _delta = viewport.rect.width - viewport.rect.height;
        _actualWidth = _height * _items.Count;
        content.sizeDelta = new Vector2(_actualWidth + _delta + Spacing * (_items.Count - 1), _height);
        for (int i = 0; i < _items.Count; i++)
        {
            var item = _items[i].GetComponent<RectTransform>();
            item.anchoredPosition = new Vector2(_delta / 2f + i * _height + _height / 2f + i * Spacing, 0);
            item.sizeDelta = new Vector2(_height, _height);
        }
        SetValue(2);
    }

    public void OnDragging()
    {
        float centerValue = Mathf.Clamp01(-content.anchoredPosition.x / (content.rect.size.x - viewport.rect.size.x)) * (_items.Count - 1.0f);
        if (centerValue < 2)
        {
            centerValue = _items.Count - 2 - (2 - centerValue);
            horizontalNormalizedPosition = centerValue / (_items.Count - 1.0f);
        }
        else if (centerValue > _items.Count - 2)
        {
            centerValue = centerValue - _items.Count + 4f;
            horizontalNormalizedPosition = centerValue / (_items.Count - 1.0f);
        }
        for (int i = 0; i < _items.Count; i++)
        {
            float value = 0;
            if (centerValue > i)
            {
                value = i - centerValue + 1;
            }
            else
            {
                value = centerValue - i + 1;
            }
            _items[i].HandleValue(value);
        }
    }
    public void SetValue(float value)
    {
        content.anchoredPosition = new Vector2(-value * (content.rect.size.x - viewport.rect.size.x) / (_items.Count - 1.0f), content.anchoredPosition.y);
    }
}
