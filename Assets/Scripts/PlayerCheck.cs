using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheck : MonoBehaviour {
    Slime slime;

    void Start()
    {
        slime = transform.parent.GetComponent<Slime>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            Debug.Log("Player entered area! Time to strike!");
            slime.Attack();
        }
    }
}