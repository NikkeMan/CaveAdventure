using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    // Changed healthMax and healthCurrent to public
    public int healthMax;
    public int healthCurrent;
    [SerializeField] private float iFrames = 1.0f;
    [SerializeField] private float iFramesMax = 1.0f;
    [SerializeField] public bool isVulnerable = true;
    public GameObject heartContainer;
    public List<Image> hearts;

    public bool playerDead = false;

    private void Start() {
        healthCurrent = healthMax;
        hearts = new List<Image>();

        for (int i = 0; i < healthMax; i++) {
            AddHeartContainer();
        }
    }

    private void Update() {
        if (iFrames <= iFramesMax && !isVulnerable) {
            iFrames += Time.deltaTime;
        }
        else if (iFrames > iFramesMax) {
            isVulnerable = true;
        }
    }

    //private void FixedUpdate() {
    //    if (iFrames < iFramesMax && !isVulnerable) {
    //        iFrames += Time.fixedDeltaTime;
    //    }
    //    else if (iFrames >= iFramesMax) {
    //        isVulnerable = true;
    //    }
    //}

    public void TakeDamage(int damageAmount) {
        if (healthCurrent > 0 && isVulnerable) {
            iFrames = 0;
            isVulnerable = false;

            for (int i = 1; i <= damageAmount; i++) {
                healthCurrent--;
                hearts[healthCurrent].fillAmount = 0;

                if (healthCurrent == 0) {
                    GameObject.Find("Player").GetComponent<Animator>().SetTrigger("PlayerDead");
                    playerDead = true;
                    break;
                }
            }
        }
        else {
            //Debug.Log("Stop it, he's already dead!");
        }
    }

    public void Heal(int healAmount) {
        if (healthCurrent < healthMax) {
            for (int i = 1; i <= healAmount; i++) {
                hearts[healthCurrent].fillAmount = 1;
                healthCurrent++;

                if (healthCurrent == healthMax) {
                    break;
                }
            }
        }
        else {
            //Debug.Log("Already at max health!");
        }
    }

    public void IncreaseMaxHealth(int increaseAmount) {
        healthMax += increaseAmount;
        AddHeartContainer();
        Heal(healthMax);
    }

    private void AddHeartContainer() {
        GameObject heart = GameObject.Instantiate(heartContainer, this.transform);
        hearts.Add(heart.transform.GetChild(0).GetComponentInChildren<Image>());
    }
}