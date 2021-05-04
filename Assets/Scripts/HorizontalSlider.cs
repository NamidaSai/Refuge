using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalSlider : MonoBehaviour
{
    [SerializeField] float backgroundScrollSpeed = 0.02f;
    Material myMaterial = default;
    Vector2 offSet = default;

    void Awake()
    {
        myMaterial = GetComponent<Renderer>().material;
    }

    void Update()
    {
        offSet = new Vector2(backgroundScrollSpeed, 0f);
        myMaterial.mainTextureOffset += offSet * Time.deltaTime;
    }
}
