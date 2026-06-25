using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Source")]
    [SerializeField] private AudioSource audioSource;

    [Header("Sound Library")]
    public List<SoundData> sounds = new List<SoundData>();

    private Dictionary<string, AudioClip> soundDict;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        BuildDictionary();
    }

    private void BuildDictionary()
    {
        soundDict = new Dictionary<string, AudioClip>();

        foreach (var s in sounds)
        {
            if (!soundDict.ContainsKey(s.soundName))
            {
                soundDict.Add(s.soundName, s.clip);
            }
        }
    }

    public void Play(string soundName)
    {
        if (soundDict.ContainsKey(soundName))
        {
            audioSource.PlayOneShot(soundDict[soundName]);
        }
        else
        {
            Debug.LogWarning("Sound not found: " + soundName);
        }
    }

    public void StopAll()
    {
        audioSource.Stop();
    }
}