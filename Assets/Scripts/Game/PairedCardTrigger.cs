using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PairedCardTrigger : MonoBehaviour
{
    private PairedCard _cardInstance;
    private void Awake()
    {
        _cardInstance = GetComponent<PairedCard>();
    }
    private void OnMouseDown()
    {
        PairsGameLogic.Instance.Select(_cardInstance);
    }
}
