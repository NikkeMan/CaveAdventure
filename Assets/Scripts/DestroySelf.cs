using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    private float destroyTimer = 0;
    [SerializeField] float destroyDelay = 1.5f;

    void Start()
    {
        destroyTimer = 0;
    }

    void Update()
    {
        destroyTimer += Time.deltaTime;

        if (destroyTimer >= destroyDelay)
        {
            Destroy(gameObject);
            destroyTimer = 0f;
        }
    }
}
