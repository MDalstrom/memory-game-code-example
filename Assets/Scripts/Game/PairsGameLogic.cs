using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Video;

public class PairsGameLogic : Singleton<PairsGameLogic>
{
    private PairedCard _firstSelectedCard;
    private PairedCard _secondSelectedCard;
    [SerializeField] private PairsGrid _pairsGrid;
    [SerializeField] private BalloonGenerator _balloonGenerator;
    private bool _isAnimating;
    private string _chosenCategory;
    [SerializeField] private int _completedPairsCount;
    private int _maxPairsCount;

    public void LoadLevel(string category)
    {
        var levelData = GameProgressSaver.Instance.GetData(category);

        var size = GameProgressSaver.Instance.GetSize(category);
        _maxPairsCount = _pairsGrid.Initialize(category, levelData.Offset, size.x, size.y) / 2;
        var completed = levelData.CompletedPairs;
        _pairsGrid.ClearOdds(completed);
        _completedPairsCount = completed.Count;

        _chosenCategory = category;
        _firstSelectedCard = null;
        _secondSelectedCard = null;
        _isAnimating = false;
    }
    public void UnloadLevel()
    {
        _pairsGrid.Deinitialize();
    }
    public void Select(PairedCard sender)
    {
        if (_isAnimating || _firstSelectedCard == sender || sender.Disposing)
            return;
        
        if (_firstSelectedCard == null)
        {
            _firstSelectedCard = sender;
            _firstSelectedCard.Show(() => { });
        }
        else if (_secondSelectedCard == null)
        {
            _isAnimating = true;
            _secondSelectedCard = sender;
            Action onCompleted;
            var cachedFirst = _firstSelectedCard;
            var cachedSecond = _secondSelectedCard;
            if (_firstSelectedCard.ID == _secondSelectedCard.ID)
            {
                GameProgressSaver.Instance.AddAction(_chosenCategory, _firstSelectedCard.ID);
                _isAnimating = false;
                _firstSelectedCard.Disposing = true;
                _secondSelectedCard.Disposing = true;
                onCompleted = () =>
                {
                    cachedFirst.Dispose(() => { });
                    cachedSecond.Dispose(OnPairCompleted);
                    AudioJockey.Instance.PlaySuccessSound();
                };
            }
            else
                onCompleted = () =>
                {
                    cachedFirst.Hide(() => _isAnimating = false );
                    cachedSecond.Hide(() => { });
                };
            _secondSelectedCard.Show(onCompleted);
            ClearSelected();
        }
    }
    private void ClearSelected()
    {
        _firstSelectedCard = null;
        _secondSelectedCard = null;
    }
    private void OnPairCompleted()
    {
        _completedPairsCount++;
        if (_completedPairsCount == _maxPairsCount)
        {
            _balloonGenerator.Invoke(() =>
            {
                if (MenuNavigation.Instance.IsPlaying)
                {
                    _completedPairsCount = 0;
                    LoadLevel(_chosenCategory);
                }
            });
        }
    }
}
