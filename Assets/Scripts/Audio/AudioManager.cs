using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] Sound[] _sounds;
    [SerializeField] string _mainTheme;

    private void Awake()
    {
        foreach (Sound sound in _sounds)
        {
            sound.AudioSource = gameObject.AddComponent<AudioSource>();
            sound.AudioSource.clip = sound.Clip;

            sound.AudioSource.volume = sound.Volume;
            sound.AudioSource.pitch = sound.Pitch;
            sound.AudioSource.loop = sound.Loop;
        }
    }

    private void Start()
    {
        Play(_mainTheme);
    }

    public void Play(string SoundName)
    {
        Sound s = Array.Find(_sounds, Sound => Sound.Name == SoundName);

        if (s == null)
        {
            Debug.LogError(SoundName + " clip not found");
            return;
        }

        s.AudioSource.Play();
    }
}
