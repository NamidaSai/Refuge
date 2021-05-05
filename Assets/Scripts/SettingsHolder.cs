using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SettingsHolder : MonoBehaviour
{
    private float sfxVolume = 1f;
    private float musicVolume = 1f;
    private float gameSpeed = 1f;


    private MusicPlayer musicPlayer;
    private AudioManager audioManager;
    private TimeManager timeManager;

    private void Start()
    {
        musicPlayer = FindObjectOfType<MusicPlayer>();
        audioManager = FindObjectOfType<AudioManager>();
        timeManager = FindObjectOfType<TimeManager>();
    }

    public float GetSFXVolume()
    {
        return sfxVolume;
    }

    public float GetMusicVolume()
    {
        return musicVolume;
    }

    public void SetSFXVolume(float value)
    {
        sfxVolume = value;
        audioManager.SetSFXVolume(value);
    }

    public void SetMusicVolume(float value)
    {
        musicVolume = value;
        musicPlayer.SetMusicVolume(value);
    }

    public float GetGameSpeed()
    {
        return gameSpeed;
    }

    public void SetGameSpeed(float value)
    {
        gameSpeed = value;
        timeManager.SetGameSpeed(value);
    }
}