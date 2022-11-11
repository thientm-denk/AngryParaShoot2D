using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioManager
{
    static bool initialized = false;
    static Dictionary<AudioName, AudioClip> audioClips = new Dictionary<AudioName, AudioClip>();
    static AudioSource audioSource;

    public static bool Initialized
    {
        get { return initialized; }
    }

    public static void Initialze(AudioSource source)
    {
        initialized = true;
        audioSource = source;
        audioClips.Add(AudioName.bowShoot, Resources.Load<AudioClip>("bowShoot"));
        audioClips.Add(AudioName.hitTarget, Resources.Load<AudioClip>("ting"));
    }

    public static void Play(AudioName name)
    {
        audioSource.PlayOneShot(audioClips[name]);

    }

}
