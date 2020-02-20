using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [Header("PickUp properties")]
    [SerializeField] bool isJumpUpgrade = false;
    [SerializeField] bool isDashUpgrade = false;
    [SerializeField] bool isHealing = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

        }
    }
}
