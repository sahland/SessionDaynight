using System;
using UnityEngine;

[System.Serializable]
public sealed class Sound
{
    [SerializeField] private string _name;
    [SerializeField] private AudioClip _clip;

    [SerializeField] private bool _loop;

    [Range(0f, 1f)]
    [SerializeField] private float _volume;

    [Range(1f, 3f)]
    [SerializeField] private float _pitch;

    AudioSource _audioSource;

    public Sound()
    {
        _loop = false;
        _volume = 1.0f;
        _pitch = 1.0f;
    }

    public AudioSource AudioSource
    {
        get { return _audioSource; }
        set { _audioSource = value; }
    }

    public string Name { get { return _name; } }
    public AudioClip Clip { get { return _clip; } }
    public float Volume { get { return _volume; } }
    public float Pitch { get { return _pitch; } }
    public bool Loop { get { return _loop; } }
}
