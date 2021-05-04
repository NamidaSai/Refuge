using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    [Tooltip("in BPM")]
    [SerializeField] float beatTempo = 120f;
    public bool hasStarted = false;

    private void Start()
    {
        beatTempo = beatTempo / 60f;
    }

    private void Update()
    {
        if (hasStarted)
        {
            transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);
        }
    }
}
