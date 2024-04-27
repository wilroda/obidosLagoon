using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class SoundManager : MonoBehaviour
{
    public enum Type { Music, Background, Fx, Voice };

    private static SoundManager _instance;

    [SerializeField] private AudioMixerGroup musicMixerOutput;
    [SerializeField] private AudioMixerGroup backgroundMixerOutput;
    [SerializeField] private AudioMixerGroup fxMixerOutput;
    [SerializeField] private AudioMixerGroup voiceMixerOutput;

    List<AudioSource> audioSources;

    public static SoundManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SoundManager>();
            }
            return _instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if ((_instance != null) && (_instance != this))
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            _instance = this;
        }

        // Find all audio sources
        audioSources = new List<AudioSource>(GetComponentsInChildren<AudioSource>());
        if (audioSources == null)
        {
            audioSources = new List<AudioSource>();
        }
    }

    private AudioSource _PlaySound(Type type, AudioClip clip, float volume = 1.0f, float pitch = 1.0f)
    {
        var audioSource = GetSource();

        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.outputAudioMixerGroup = GetMixer(type);

        audioSource.Play();

        return audioSource;
    }

    private AudioMixerGroup GetMixer(Type type)
    {
        switch (type)
        {
            case Type.Music:
                return musicMixerOutput;
            case Type.Background:
                return backgroundMixerOutput;
            case Type.Fx:
                return fxMixerOutput;
            case Type.Voice:
                return voiceMixerOutput;
        }
        return fxMixerOutput;
    }

    private AudioSource GetSource()
    {
        if (audioSources == null)
        {
            audioSources = new List<AudioSource>();
            return NewSource();
        }

        foreach (var source in audioSources)
        {
            if (!source.isPlaying)
            {
                return source;
            }
        }

        return NewSource();
    }

    private AudioSource NewSource()
    {
        GameObject go = new GameObject();
        go.name = "Audio Source";
        go.transform.SetParent(transform);

        var audioSource = go.AddComponent<AudioSource>();

        audioSources.Add(audioSource);

        return audioSource;
    }

    static public AudioSource PlaySound(Type type, AudioClip clip, float volume = 1.0f, float pitch = 1.0f)
    {
        return _instance._PlaySound(type, clip, volume, pitch);
    }
}
