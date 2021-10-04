using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PairsGrid : MonoBehaviour
{
    [SerializeField] private GameObject _cardPrefab;
    private List<PairedCard> _cardsInstances;
    [Header("Grid Params")]
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private float _maxCardSize;
    [SerializeField] private float _maxHeight;
    [SerializeField] private float _spacing;
    private bool _isInitialized;

    public int Initialize(string category, int iconOffset, int width, int height)
    {
        if (_isInitialized)
            Deinitialize();

        _cardsInstances = new List<PairedCard>();
        var cardSize = Mathf.Clamp((_maxHeight - (height - 1) * _spacing) / height, 0, _maxCardSize);

        for (int xIndex = 0; xIndex < width; xIndex++)
        {
            float convertedX = -(width / 2f - 0.5f) + xIndex;
            float resultX = convertedX * cardSize + convertedX * _spacing;
            for (int yIndex = 0; yIndex < height; yIndex++)
            {
                float convertedY = -(height / 2f - 0.5f) + yIndex;
                float resultY = convertedY * cardSize + convertedY * _spacing;

                var cardInstance = Instantiate(_cardPrefab, transform).GetComponent<PairedCard>();
                _cardsInstances.Add(cardInstance);

                cardInstance.transform.localScale = Vector3.one * cardSize;
                cardInstance.transform.localPosition = new Vector3(resultX, resultY, 0);
            }
        }

        _cardsInstances.Shuffle();
        for (int i = 0; i < _cardsInstances.Count; i += 2)
        {
            int absoluteID = i / 2;
            string id = absoluteID.ToString();
            _cardsInstances[i].ID = id;
            _cardsInstances[i + 1].ID = id;

            var relativeID = absoluteID + iconOffset;
            if (category.ToLower() == "zodiac")
                relativeID %= 12;
            else
                relativeID %= 20;
            relativeID++;
            string path = $"{category}/{relativeID}";
            new CardIconLoadingRequest(_cardsInstances[i], _cardsInstances[i + 1], path);
        }

        _isInitialized = true;
        return _cardsInstances.Count;
    }
    public void ClearOdds(IReadOnlyList<string> oddIDs)
    {
        _cardsInstances.Where(x => oddIDs.Contains(x.ID)).ToList().ForEach(x => Destroy(x.gameObject));
    }
    public void Deinitialize()
    {
        _cardsInstances.ForEach(x =>
        {
            if (x != null)
            {
                Destroy(x.gameObject);
            }
        });
        _cardsInstances.Clear();
        _isInitialized = false;
    }
}
public static class ListExtensions
{
    public static void Shuffle<T>(this IList<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(0, list.Count);
            T local = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = local;
        }
    }
    public static string ToSingleString<T>(this IList<T> list)
    {
        string result = "{ ";
        list.ToList().ForEach(x => { 
            result += x;
            result += ',';
        });
        result = result.Remove(result.Length - 1);
        result += " }";
        return result;
    }
}
