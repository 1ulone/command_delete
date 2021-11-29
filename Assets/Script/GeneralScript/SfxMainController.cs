using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxMainController : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip _attack, _encounter, _itempickup, _heal, _commandenter, _error;

    void Start()
    {
        SFXController.audioSource = audioSource;
        SFXData.attack = _attack;
        SFXData.encounter = _encounter;
        SFXData.itempickup = _itempickup;
        SFXData.heal = _heal;
        SFXData.commandenter = _commandenter;
        SFXData.error = _error;
    }
}
