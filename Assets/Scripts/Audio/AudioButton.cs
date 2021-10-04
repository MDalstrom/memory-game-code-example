using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour
{
    [SerializeField] private Image _buttonImage;
    [SerializeField] private Sprite _musicOnSprite;
    [SerializeField] private Sprite _musicOffSprite;
    private bool _state;

    private void Start()
    {
        _state = PlayerPrefs.GetInt("MusicOff") == 0;
        UpdateSprite();
        if (_state)
            AudioJockey.Instance.Play();
        else
            AudioJockey.Instance.Stop();
    }
    public void Press()
    {
        _state = !_state;
        if (_state)
            AudioJockey.Instance.Play();
        else
            AudioJockey.Instance.Stop();
        PlayerPrefs.SetInt("MusicOff", _state ? 0 : 1);
        UpdateSprite();
    }
    private void UpdateSprite()
    {
        _buttonImage.sprite = _state ? _musicOnSprite : _musicOffSprite;
    }
}
