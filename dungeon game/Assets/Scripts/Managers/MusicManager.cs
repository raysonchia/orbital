using UnityEngine;
using System.Collections;

public class MusicManager : Singleton<MusicManager>
{
    [SerializeField]
    float transitionTime = 1.25f;
    private AudioSource audioSource;
    private AudioClip previousMusic;

    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
    }

    public void ChangeMusic(AudioClip newClip)
    {
        previousMusic = audioSource.clip;
        StartCoroutine(TransitionRoutine(newClip));
    }

    public void SetPreviousMusic()
    {
        StartCoroutine(TransitionRoutine(previousMusic));
    }

    private IEnumerator TransitionRoutine(AudioClip newClip)
    {
        float originalVolume = audioSource.volume;

        for (var timePassed = 0f; timePassed < transitionTime; timePassed += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(originalVolume, 0f, timePassed / transitionTime);
            yield return null;
        }

        audioSource.volume = 0f;
        audioSource.clip = newClip;
        yield return new WaitForSeconds(0.1f);
        audioSource.Play();

        for (var timePassed = 0f; timePassed < transitionTime; timePassed += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(0f, originalVolume, timePassed / transitionTime);
            yield return null;
        }

        audioSource.volume = originalVolume;
    }
}