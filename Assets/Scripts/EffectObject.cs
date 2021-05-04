using UnityEngine;

public class EffectObject : MonoBehaviour
{
    [SerializeField] float lifetime = 2f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }
}