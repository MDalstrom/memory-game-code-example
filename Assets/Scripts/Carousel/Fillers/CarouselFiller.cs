using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CarouselFiller : MonoBehaviour
{
    public static CarouselFiller Instance { private set; get; }

    [SerializeField] protected Carousel _carousel;
    [SerializeField] protected GameObject _itemPrefab;
    [SerializeField] private string[] _foldersPaths;
    protected List<ICarouselItem> _carouselItems;
    
    protected virtual IEnumerator Start()
    {
        Instance = this;
        var foldersPaths = _foldersPaths;
        _carouselItems = new List<ICarouselItem>();
        for (int i = foldersPaths.Length - 2; i < _foldersPaths.Length; i++)
        {
            yield return StartCoroutine(AddCategory(foldersPaths[i]));
        }
        for (int i = 0; i < foldersPaths.Length; i++)
        {
            yield return StartCoroutine(AddCategory(foldersPaths[i]));
        }
        for (int i = 0; i < 2; i++)
        {
            yield return StartCoroutine(AddCategory(foldersPaths[i]));
        }
        _carousel.Initialize();
    }
    protected abstract IEnumerator AddCategory(string categoryName);
}
