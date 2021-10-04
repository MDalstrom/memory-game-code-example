using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoriesLibrary : Singleton<CategoriesLibrary>
{
    [SerializeField] private string[] _categories;
    public string[] Categories => _categories;
    [SerializeField] private Vector2Int[] _gridSizes;
    public Vector2Int[] GridSizes => _gridSizes;
}
