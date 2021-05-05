using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButton : MonoBehaviour
{
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
            GetComponent<Animator>().SetTrigger("Pressed");
        }

        if (Input.GetKeyUp(keyToPress))
        {
        }
    }
}
