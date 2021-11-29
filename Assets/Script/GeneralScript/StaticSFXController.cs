using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SFXController
{
    public static AudioSource audioSource;

    public static void PlaySFX(AudioClip ac)   
        => audioSource.PlayOneShot(ac);
}

public class SFXData
{
    public static AudioClip attack
        ,encounter
        ,itempickup
        ,heal
        ,commandenter
        ,error;
}