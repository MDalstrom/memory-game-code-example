using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCategoriesFiller : LockableCarouselFiller
{
    protected override IEnumerator AddCategory(string categoryName)
    {
        var introLoadRequest = Resources.LoadAsync($"{categoryName}/intro");
        yield return introLoadRequest;

        var item = Instantiate(_itemPrefab, _carousel.content).GetComponent<GameCategoryItem>();
        item.CategoryName = categoryName;
        item.Initialize(introLoadRequest.asset as Texture2D);

        _carouselItems.Add(item);
    }
}
