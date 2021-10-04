using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class BalloonGenerator : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private EdgeCollider2D[] _walls;
    [SerializeField] private Sprite[] _balloons;

    [Header("Settings")]
    [SerializeField] private int _amount;

    [SerializeField] private List<GameObject> _balloonInstances;
    private System.Action _callback;
    private float _halfHeight;
    private float _halfWidth;

    private void Start()
    {
        _balloonInstances = new List<GameObject>();

        var ratio = Screen.width / (float)Screen.height;
        _halfHeight = Camera.main.orthographicSize;
        _halfWidth = ratio * _halfHeight;

        var height = _halfHeight * 4f;
        _walls[0].SetPoints(new List<Vector2> { new Vector2(-_halfWidth, height), new Vector2(-_halfWidth, -height) });
        _walls[1].SetPoints(new List<Vector2> { new Vector2(_halfWidth, height), new Vector2(_halfWidth, -height) });

        MenuNavigation.Instance.PressedBack += Interrupt;
    }
    public void Invoke(System.Action callback)
    {
        for (int i = 0; i < _amount; i++)
        {
            _balloonInstances.Add(CreateBalloon());
        }
        _callback = callback;
        AudioJockey.Instance.PlayClappingSound();
    }
    public void Interrupt()
    {
        _balloonInstances.ForEach(x => Destroy(x));
        _balloonInstances.Clear();
    }
    private void Update()
    {
        if (_balloonInstances.Count == 0)
            return;
        if (_balloonInstances.First() is var first && first != null && first.transform.position.y < -_halfHeight * 1.1f)
        {
            Destroy(first);
            _balloonInstances.Remove(first);
            if (_balloonInstances.Count == 0)
                _callback?.Invoke();
        }
    }
    private GameObject CreateBalloon()
    {
        GameObject balloon = new GameObject();
        balloon.transform.parent = transform;
        balloon.transform.position = new Vector3(
            Random.Range(-_halfWidth, _halfWidth), 
            Random.Range(_halfHeight * 1.1f, _halfHeight * 3.1f), 
            0);
        balloon.transform.eulerAngles = new Vector3(0, 0, Random.Range(0f, 360f));

        balloon.AddComponent<BoxCollider2D>();
        var handler = balloon.AddComponent<ClickHandler>();
        handler.Pressed = new UnityEngine.Events.UnityEvent();
        handler.Pressed.AddListener(() =>
        {
            _balloonInstances.Remove(balloon);
            Destroy(balloon);
            AudioJockey.Instance.PlayBlobSound();
        });

        var sr = balloon.AddComponent<SpriteRenderer>();
        sr.sortingOrder = 100;
        sr.sprite = _balloons[Random.Range(0, _balloons.Length)];

        var rb2d = balloon.AddComponent<Rigidbody2D>();
        rb2d.AddForce(Vector2.down, ForceMode2D.Impulse);
        rb2d.drag = 0.0f;
        rb2d.angularDrag = 0.0f;
        rb2d.gravityScale = 0.4f;
        return balloon;
    }
}
