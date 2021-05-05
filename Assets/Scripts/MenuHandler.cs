using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] GameObject tutorialMenu = default;
    [SerializeField] GameObject optionsMenu = default;
    [SerializeField] Slider sfxVolumeSlider = default;
    [SerializeField] Slider musicVolumeSlider = default;
    [SerializeField] Slider gameSpeedSlider = default;
    [SerializeField] AudioClip sampleMusic = default;


    bool isOptionsMenu = false;
    bool isTutorialMenu = false;

    SettingsHolder settings;
    AudioSource sampleMusicSource;

    private void Start()
    {
        sampleMusicSource = GetComponent<AudioSource>();
        settings = FindObjectOfType<SettingsHolder>();
        optionsMenu.SetActive(false);
        tutorialMenu.SetActive(false);
        AddListeners();
        ResetParameters();
    }

    private void AddListeners()
    {
        sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        gameSpeedSlider.onValueChanged.AddListener(SetGameSpeed);
    }

    private void ResetParameters()
    {
        sfxVolumeSlider.value = settings.GetSFXVolume();
        musicVolumeSlider.value = settings.GetMusicVolume();
        gameSpeedSlider.value = settings.GetGameSpeed();
        musicVolumeSlider.onValueChanged.AddListener(PlaySampleMusic);
    }

    public void GoToOptionsMenu()
    {
        if (isTutorialMenu)
        {
            GoToTutorialMenu();
        }

        if (!isOptionsMenu)
        {
            optionsMenu.SetActive(true);
            isOptionsMenu = true;
        }
        else
        {
            optionsMenu.SetActive(false);
            isOptionsMenu = false;
        }
    }

    public void GoToTutorialMenu()
    {
        if (isOptionsMenu)
        {
            GoToOptionsMenu();
        }

        if (!isTutorialMenu)
        {
            tutorialMenu.SetActive(true);
            isTutorialMenu = true;
        }
        else
        {
            tutorialMenu.SetActive(false);
            isTutorialMenu = false;
        }
    }

    public void SetSFXVolume(float value)
    {
        settings.SetSFXVolume(value);
    }

    public void SetMusicVolume(float value)
    {
        settings.SetMusicVolume(value);
    }

    public void SetGameSpeed(float value)
    {
        settings.SetGameSpeed(value);
    }

    public void PlaySampleMusic(float value)
    {
        if (sampleMusicSource.isPlaying)
        {
            sampleMusicSource.Stop();
        }

        sampleMusicSource.PlayOneShot(sampleMusic, value);
    }
}