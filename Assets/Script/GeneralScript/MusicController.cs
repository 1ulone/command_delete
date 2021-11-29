using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static MusicController instance;

    [SerializeField]
    private AudioClip[] music;
    [SerializeField]
    private AudioSource audioSource;

    int currIndex;

    void Awake()
        => instance = this;

    void Start()
    {
        currIndex = 1;
        MusicChanger.ChangeMusic(0, true);
    }

    public void MusicToIndex(int i, bool b)
    {
        if (currIndex != i)
        {
            audioSource.loop = b;
            audioSource.clip = music[i];
            audioSource.Play();
        }
        currIndex = i;
    }

    public void MusicEnd()
        => audioSource.Stop();
}

public static class MusicChanger
{
    public static void ChangeMusic(int i, bool b)
        => MusicController.instance.MusicToIndex(i, b);

    public static void StopMusic()
        => MusicController.instance.MusicEnd();
}
