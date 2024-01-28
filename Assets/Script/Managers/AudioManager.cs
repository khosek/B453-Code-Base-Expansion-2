using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    public AudioSource musicAudioSource;

    public AudioSource sfxAudioSource;

    [SerializeField] private AudioClip[] clips;

    [SerializeField] private AudioClip gameOverClip;

    [SerializeField] private AudioClip pickupClip;

    [SerializeField] private AudioClip dropClip;

    [SerializeField] private AudioClip noClip;

    private Coroutine _audioCoroutine;


    private int currentClip = 0;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        Play();
    }

    public void Play()
    {
        if (!musicAudioSource || clips.Length == 0) return;

        _audioCoroutine = StartCoroutine(LoopClips());
    }

    private void PlayClip(AudioSource selectedAudioSource, AudioClip newClip, bool isLoop = false)
    {
        if (selectedAudioSource == null || newClip == null) return;

        selectedAudioSource.clip = newClip;

        selectedAudioSource.loop = isLoop;

        selectedAudioSource.Play();
    }

    public void PlayGameOver()
    {
        PlayClip(musicAudioSource, gameOverClip, true);
    }

    public void PlayPickup()
    {
        PlayClip(sfxAudioSource, pickupClip);
    }

    public void PlayDrop()
    {
        PlayClip(sfxAudioSource, dropClip);
    }

    public void PlayNo()
    {
        PlayClip(sfxAudioSource, noClip);
    }

    public void Stop()
    {

        if (!musicAudioSource || clips.Length == 0 || _audioCoroutine == null) return;

        StopCoroutine(_audioCoroutine);

        musicAudioSource.Stop();

        _audioCoroutine = null;
    }

    private IEnumerator LoopClips()
    {
        while (true)
        {
            // audioSource.clip = clips[RcurrentClip];
            musicAudioSource.clip = clips[Random.Range(0, clips.Length)];
            musicAudioSource.Play();

            yield return new WaitForSeconds(musicAudioSource.clip.length);

            currentClip++;


            if (currentClip >= clips.Length)
                currentClip = 0;
        }

    }
    private void Update()
    {
        if (Input.GetButton("Jump"))
        {
            Stop();
            Play();
        }
    }
}
