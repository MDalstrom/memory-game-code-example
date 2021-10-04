using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioJockey : Singleton<AudioJockey>
{
    [Header("Ambients")]
    [SerializeField] private AudioClip[] _ambients;
    [SerializeField] private AudioSource _ambientSource;
    private int _currentAmbientIndex;
    private Coroutine _delayRoutine;
    [Header("Sounds")]
    [SerializeField] private AudioSource _success; 
    [SerializeField] private AudioSource _clapping; 
    [SerializeField] private AudioSource _blob; 

    public void Unmute()
    {
        _ambientSource.volume = 1f;
    }
    public void Play()
    {
        _currentAmbientIndex = Random.Range(0, _ambients.Length);
        UpdateAmbient();
        _delayRoutine = StartCoroutine(DelayPlaying());
    }
    public void Stop()
    {
        _ambientSource.Pause();
        if (_delayRoutine != null)
            StopCoroutine(_delayRoutine);
    }
    private IEnumerator DelayPlaying()
    {
        float remainingTime = _ambients[_currentAmbientIndex].length - _ambientSource.time;
        yield return new WaitForSeconds(remainingTime);
        NextAmbient();
    }

    private void NextAmbient()
    {
        _currentAmbientIndex++;
        if (_currentAmbientIndex == _ambients.Length)
            _currentAmbientIndex = 0;
        UpdateAmbient();
    }
    private void UpdateAmbient()
    {
        _ambientSource.clip = _ambients[_currentAmbientIndex];
        _ambientSource.Play();
    }

    public void PlaySuccessSound()
    {
        _success.Play();
    }
    public void PlayClappingSound()
    {
        _clapping.Play();
    }
    public void PlayBlobSound()
    {
        _blob.Play();
    }
}
