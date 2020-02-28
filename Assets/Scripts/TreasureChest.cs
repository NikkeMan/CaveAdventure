using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    [SerializeField] int scoreAmount = 500;
    [SerializeField] bool hasKey = false;
    Score score;
    Animator animator;

    [SerializeField] DataLinker dataLinker;
    [SerializeField] GameObject inventoryItem;
    [SerializeField] int itemsToSpawn = 1;

    // Start is called before the first frame update
    void Start()
    {
        score = GameObject.Find("ScorePanel").GetComponent<Score>();
        animator = GetComponent<Animator>();
        dataLinker = GameObject.Find("GameManager").GetComponent<DataLinker>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("PlayerAttackBox")) {
            score.AddToScore(scoreAmount);
            animator.SetBool("isOpen", true);

            if (inventoryItem != null)
            {
                for (int i = 0; i < itemsToSpawn; i++)
                {
                    Instantiate(inventoryItem, transform.position, Quaternion.identity);
                }
            }
            

            gameObject.GetComponent<CircleCollider2D>().enabled = false;

            if (hasKey) {
                //add a key to inventory:
                //Debug.Log("You got a key. Nice!");
            }
        }
    }
}