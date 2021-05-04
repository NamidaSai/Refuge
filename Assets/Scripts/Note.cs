using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField] bool canBePressed = false;
    [SerializeField] bool wasTriggered = false;
    [SerializeField] KeyCode keyToPress;
    [SerializeField] float normalThreshold = 0.25f;
    [SerializeField] float goodThreshold = 0.05f;

    [SerializeField] GameObject hitEffect, goodEffect, perfectEffect, missEffect;

    private void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                wasTriggered = true;
                gameObject.SetActive(false);
                HandleHitQuality();
            }
        }
    }

    private void HandleHitQuality()
    {
        float triggeredYPosition = Mathf.Abs(transform.position.y);
        if (triggeredYPosition > normalThreshold)
        {
            GameManager.instance.NormalHit();
            Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
        }
        else if (triggeredYPosition > goodThreshold)
        {
            GameManager.instance.GoodHit();
            Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
        }
        else
        {
            GameManager.instance.PerfectHit();
            Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator" && !wasTriggered)
        {
            canBePressed = false;

            GameManager.instance.NoteMissed();
            Instantiate(missEffect, transform.position, missEffect.transform.rotation);
        }
    }
}

