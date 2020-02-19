using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] int healthMax;
    [SerializeField] int healthCurrent;
    public GameObject heartContainer;
    public List<Image> hearts;


    // Start is called before the first frame update
    void Start()
    {
        healthCurrent = healthMax;
        hearts = new List<Image>();

        for (int i = 0; i < healthMax; i++ ) {
            AddHeartContainer();
        }

        Debug.Log("Your max health is " + healthMax);
        Debug.Log("Your current health is " + healthCurrent);
    }

    public void TakeDamage(int damageAmount) {
        if (healthCurrent > 0) {
            for (int i = 1; i <= damageAmount; i++) {
                healthCurrent--;
                Debug.Log("Your current health is " + healthCurrent);
                hearts[healthCurrent].fillAmount = 0;

                if (healthCurrent == 0) {
                    Debug.Log("You died, lol!");
                    break;
                }
            }
        }
        else {
            Debug.Log("Stop it, he's already dead!");
        }
    }

    public void Heal(int healAmount) {
        if(healthCurrent < healthMax) {
            for (int i = 1; i <= healAmount; i++) {
                hearts[healthCurrent].fillAmount = 1;
                healthCurrent++;
                Debug.Log("Your current health is " + healthCurrent);

                if (healthCurrent == healthMax) {
                    Debug.Log("MAX HEALTH WOW");
                    break;
                }
            }
        }
        else {
            Debug.Log("Already at max health!");
        }
    }

    public void IncreaseMaxHealth(int increaseAmount) {
        healthMax += increaseAmount;
        AddHeartContainer();
        Debug.Log("Your max health is now " + healthMax);
        Heal(healthMax);
    }

    void AddHeartContainer() {
        GameObject heart = GameObject.Instantiate(heartContainer, this.transform);
        hearts.Add(heart.transform.GetChild(0).GetComponentInChildren<Image>());
    }
}

public class Player : MonoBehaviour {

}


//public void TakeDamage(int dmg) {
//    for (int i = 0; i < dmg; i++) {
//        health -= 1;
//        ClampHealth();
//    }
//}

//public void Heal(int heal) {
//    for (int i = 0; i < heal; i++) {
//        health += 1;
//        ClampHealth();
//    }
//}