using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Lava : MonoBehaviour
{
    [SerializeField] float hurtBoxTimer = 1.0f;
    [SerializeField] float hurtBoxTimerMax = 1.0f;
    HealthBar healthBar;
    int damageAmount = 1;
    [SerializeField] TilemapCollider2D tileMapCollider;

    void Start()
    {
        healthBar = GameObject.Find("HealthPanel").GetComponent<HealthBar>();
        tileMapCollider = gameObject.GetComponent<TilemapCollider2D>();
    }

    private void FixedUpdate() {
        if (hurtBoxTimer < hurtBoxTimerMax) {
            hurtBoxTimer += Time.deltaTime;
        }
        if (hurtBoxTimer >= hurtBoxTimerMax && tileMapCollider.isActiveAndEnabled == false) {
            tileMapCollider.enabled = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            healthBar.TakeDamage(damageAmount);
            tileMapCollider.enabled = false;
            hurtBoxTimer = 0;
        }
    }
}
