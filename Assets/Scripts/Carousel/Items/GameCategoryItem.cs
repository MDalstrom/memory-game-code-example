using UnityEngine;
using UnityEngine.UI;

public class GameCategoryItem : LockableItem
{
    private Vector2 _initialSize;
    [Range(0f, 2f)] [SerializeField] private float _minScalingCoef = 0.75f;
    [Range(0f, 2f)] [SerializeField] private float _maxScalingCoef = 1.25f;

    public void Initialize(Texture2D categoryTexture)
    {
        _imageHolder.texture = categoryTexture;
        var imageTransform = _imageHolder.transform as RectTransform;
        var frameTransform = transform as RectTransform;

        var previewRatio = (float)categoryTexture.width / categoryTexture.height;
        var maxSide = (GetComponentInParent<Canvas>().transform as RectTransform).sizeDelta.y - 260f;

        float width, height;
        if (previewRatio > 1)
        {
            width = maxSide;
            height = maxSide / previewRatio;
        }
        else
        {
            width = maxSide * previewRatio;
            height = maxSide;
        }

        var newSize = new Vector2(width, height);
        frameTransform.sizeDelta = new Vector2(maxSide, maxSide);
        imageTransform.sizeDelta = newSize;
        _initialSize = newSize;

        _imageHolder.GetComponent<Button>().onClick.AddListener(Open);
    }
    public override void HandleValue(float value)
    {
        (_imageHolder.transform as RectTransform).sizeDelta = Vector2.Lerp(_initialSize * _minScalingCoef, _initialSize * _maxScalingCoef, value);
    }

    protected override void OnLockedOpened()
    {
        MenuNavigation.Instance.SafelyOpenPurchasing();
    }

    protected override void OnUnlockedOpened()
    {
        MenuNavigation.Instance.OpenGameInterface();
        PairsGameLogic.Instance.LoadLevel(CategoryName);
    }
}
