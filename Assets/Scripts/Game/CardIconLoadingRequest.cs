using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardIconLoadingRequest
{
    private PairedCard _firstCard;
    private PairedCard _secondCard;
    private string _path;
    public CardIconLoadingRequest(PairedCard first, PairedCard second, string path)
    {
        _firstCard = first;
        _secondCard = second;
        _path = path;

        _firstCard.StartCoroutine(Downloading());
    }

    private IEnumerator Downloading()
    {
        var request = Resources.LoadAsync<Sprite>(_path);
        yield return request;
        SetIcons(request.asset as Sprite);
    }
    private void SetIcons(Sprite icon)
    {
        _firstCard.SetIcon(icon);
        _secondCard.SetIcon(icon);
    }
}
