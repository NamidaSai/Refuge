using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButton : MonoBehaviour
{
    [SerializeField] KeyCode keyToPress = default;

    private SpriteRenderer spriteRenderer;
    AudioManager audioManager;


    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            GetComponent<Animator>().SetTrigger("Pressed");
            audioManager.Play("pluck");
        }
    }
}
