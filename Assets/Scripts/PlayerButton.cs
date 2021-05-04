using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButton : MonoBehaviour
{
    [SerializeField] Sprite defaultSprite = default;
    [SerializeField] Sprite pressedSprite = default;
    [SerializeField] KeyCode keyToPress = default;

    private SpriteRenderer spriteRenderer;


    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            spriteRenderer.sprite = pressedSprite;
        }

        if (Input.GetKeyUp(keyToPress))
        {
            spriteRenderer.sprite = defaultSprite;
        }
    }

}
