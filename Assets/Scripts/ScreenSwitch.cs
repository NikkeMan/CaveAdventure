using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSwitch : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject fromCamera;
    [SerializeField] GameObject toCamera;
    [SerializeField] DataLinker dataLinker;
    [SerializeField] GameObject player;
    [SerializeField] Animator playerAnimator;

    bool transitioning = false;
    [SerializeField] float direction;
    [SerializeField] float moveTime = 1.5f;
    [SerializeField] float timer;

    private void Start()
    {
        timer = moveTime;
        toCamera.SetActive(false);

        playerAnimator = dataLinker.playerRef.gameObject.GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        if (transitioning)
        {
            transitionMove();

            playerAnimator.SetFloat("horizontal", 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (fromCamera.activeSelf)
            {
                fromCamera.SetActive(false);
                toCamera.SetActive(true);
                direction = 1;
                timer = moveTime;

                transitioning = true;
            }

            else
            {
                fromCamera.SetActive(true);
                toCamera.SetActive(false);
                direction = -1;

                transitioning = true;
                timer = moveTime;
            }
        }
    }

    void transitionMove()
    {
        timer -= Time.deltaTime;

        if (timer > 0)
        {
            dataLinker.playerMovement.MovePlayer(direction);

            dataLinker.playerMovement.playerInputsDisabled = true;
        }

        else
        {
            transitioning = false;
            dataLinker.playerMovement.playerInputsDisabled = false;
        }
    }
}
