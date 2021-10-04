using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameProgressSaver : Singleton<GameProgressSaver>
{
    private List<LevelData> _completedPairs;
    public event Action Completed;
    private void Start()
    {
        _completedPairs = new List<LevelData>();
        CategoriesLibrary.Instance.Categories.ToList().ForEach(x =>
        {
            var data = new LevelData(x);
            data.Completed += Completed;
            _completedPairs.Add(data);
        });
    }
    public void AddAction(string category, string id)
    {
        GetData(category).AddData(id);
    }
    public LevelData GetData(string category)
    {
        return _completedPairs.Where(x => x.Category == category).First();
    }
    public IReadOnlyList<string> GetActions(string category)
    {
        return GetData(category).CompletedPairs;
    }
    public Vector2Int GetSize(string category)
    {
        return CategoriesLibrary.Instance.GridSizes[GetData(category).Level];
    }
}
public class LevelData
{
    public int Level { get; private set; }
    public string Category { get; private set; }
    public int Offset { get; private set; }

    private List<string> _completedPairs;
    public IReadOnlyList<string> CompletedPairs => _completedPairs;
    public event Action Completed;

    public LevelData(string category)
    {
        Level = 0;
        Category = category;
        Offset = UnityEngine.Random.Range(0, 20);
        _completedPairs = new List<string>();
    }

    public void AddData(string id)
    {
        _completedPairs.Add(id);
        int levelSize = CategoriesLibrary.Instance.GridSizes[Level].x * CategoriesLibrary.Instance.GridSizes[Level].y;
        if (_completedPairs.Count == levelSize / 2)
            NextLevel();
    }
    private void NextLevel()
    {
        Level++;
        if (CategoriesLibrary.Instance.GridSizes.Length == Level ||
            (Category == "Zodiac" && Level == 6))
            Level = 0;
        _completedPairs.Clear();
        Completed?.Invoke();
    }
}
