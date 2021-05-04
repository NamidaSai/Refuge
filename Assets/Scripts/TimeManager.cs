using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [Range(0.1f, 3f)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] AudioSource currentTrack = default;
    void Update()
    {
        Time.timeScale = gameSpeed;
        currentTrack.pitch = gameSpeed;
    }
}
