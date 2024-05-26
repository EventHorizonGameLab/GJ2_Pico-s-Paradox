using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    //Events
    public static Action OnGustavo;
    public static Action OnEntertingLastRoom;
    public static Action OnEndGame;


    [SerializeField] AudioData audioData;
    AudioClip base_BGM;
    AudioClip final_BGM;
    AudioSource AudioSource;

    private void Awake()
    {
        base_BGM = audioData.music_BGM;
        final_BGM = audioData.music_Gustavo;
        AudioSource = GetComponentInChildren<AudioSource>();
        AudioSource.clip = base_BGM;
    }

    private void OnEnable()
    {
        OnGustavo += ChangeBGM;
        OnEntertingLastRoom += StopBGM;
        OnEndGame += StopBGM;
    }

    private void OnDisable()
    {
        OnGustavo -= ChangeBGM;
        OnEntertingLastRoom -= StopBGM;
        OnEndGame -= StopBGM;
    }

    private void Start()
    {
        AudioSource.loop = true;
        AudioSource.Play();
    }

    void ChangeBGM()
    {
        AudioSource.Stop();
        AudioSource.clip = final_BGM;
        AudioSource.Play();
    }

    void StopBGM()
    {
        AudioSource.Stop();
    }
}
