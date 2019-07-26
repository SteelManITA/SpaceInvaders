using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    private AudioSource _soundTrack;

    public AudioClip soundTrack;
    public AudioClip shot;
    public AudioClip explosion;

    private AudioSource newSound(AudioClip clip, float volume, bool loop)
    {
        AudioSource audio = gameObject.AddComponent<AudioSource>();
        audio.clip = clip;
        audio.volume = volume;
		audio.loop = loop;
        return audio;
    }

    private AudioSource newSound(AudioClip clip, float volume)
    {
        AudioSource audio = gameObject.AddComponent<AudioSource>();
        audio.clip = clip;
        audio.volume = volume;
		audio.loop = false;
        return audio;
    }

    private AudioSource newSound(AudioClip clip)
    {
        AudioSource audio = gameObject.AddComponent<AudioSource>();
        audio.clip = clip;
        audio.volume = 1f;
		audio.loop = false;
        return audio;
    }

    public static SoundManager getInstance()
    {
        return instance;
    }

    private SoundManager()
    {
    }

    void Awake()
    {
        instance = this;
        this._soundTrack = newSound(
            this.soundTrack,
            (float) (PlayerPrefs.GetInt("Music", 1)),
            true
        );
        this._soundTrack.Play();
    }

    void Update()
    {
        this._soundTrack.volume = (float) (PlayerPrefs.GetInt("Music", 1));
    }

    private IEnumerator DestroyAudio(AudioSource audio)
    {
        yield return new WaitForSeconds(audio.clip.length);
        Destroy(audio);
    }

    public void Shot() {
        AudioSource audio = newSound(
            this.shot,
            (float) (Convert.ToBoolean(PlayerPrefs.GetInt("Sound", 1)) ? 0.2f : 0f)
        );
        audio.Play();
        StartCoroutine(DestroyAudio(audio));
    }

    public void Explosion() {
        AudioSource audio = newSound(
            this.explosion,
            (float) (PlayerPrefs.GetInt("Sound", 1))
        );
        audio.Play();
        StartCoroutine(DestroyAudio(audio));
    }
}
