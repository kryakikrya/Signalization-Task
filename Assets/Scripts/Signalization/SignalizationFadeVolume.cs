using DG.Tweening;
using UnityEngine;
public sealed class Signalization : MonoBehaviour
{
    [SerializeField] private AudioSource _source;
    [SerializeField] private float _fadeDuration;
    [SerializeField] private float _targetVolume;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            if (_source.isPlaying == false)
            {
                _source.Play();
            }
            FadeVolume(_targetVolume);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            FadeVolume(0);
        }
    }

    public void FadeVolume(float targetVolume)
    {
        _source.DOKill();
        var fade = _source.DOFade(targetVolume, _fadeDuration).SetEase(Ease.Linear).SetUpdate(true).SetLink(_source.gameObject);
    }
}
