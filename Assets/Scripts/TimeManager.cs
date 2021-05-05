using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [Range(0.1f, 3f)] [SerializeField] float gameSpeed = 1f;

    void Update()
    {
        Time.timeScale = gameSpeed;
        FindObjectOfType<MusicPlayer>().GetComponent<AudioSource>().pitch = gameSpeed;
    }

    public void SetGameSpeed(float value)
    {
        if (value == 0f)
        {
            gameSpeed = 0.5f;
        }
        else
        {
            gameSpeed = value;
        }
    }
}
