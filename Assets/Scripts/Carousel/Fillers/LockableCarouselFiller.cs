using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class LockableCarouselFiller : CarouselFiller
{
    protected override IEnumerator Start()
    {
        yield return base.Start();
        if (StoreListener.Instance.AccessPurchased)
            Unlock();
        else
        {
            Lock();
            StoreListener.Instance.PurchasedSuccessfully += (sender, args) => Unlock();
        }
    }
    public void Unlock()
    {
        _carouselItems.Select(x => x as GameCategoryItem).ToList().ForEach(x => x.Unlock());
    }
    public void Lock()
    {
        _carouselItems.Select(x => x as GameCategoryItem).ToList().ForEach(x =>
        {
            if (x.CategoryName == "Alphabet" || x.CategoryName == "Pirate")
                x.Unlock();
            else
                x.Lock();
        });
    }
}
