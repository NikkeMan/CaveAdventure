using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    [SerializeField] int scoreAmount = 500;
    [SerializeField] bool hasKey = false;
    Score score;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        score = GameObject.Find("ScorePanel").GetComponent<Score>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            score.AddToScore(scoreAmount);
            animator.SetBool("isOpen", true);
            gameObject.GetComponent<CircleCollider2D>().enabled = false;

            if (hasKey) {
                //add a key to inventory:
                //Debug.Log("You got a key. Nice!");
            }
        }
    }
}