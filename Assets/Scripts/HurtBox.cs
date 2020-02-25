using UnityEngine;

public class HurtBox : MonoBehaviour {
    [SerializeField] float hurtBoxTimer = 1.0f;
    [SerializeField] float hurtBoxTimerMax = 1.0f;
    HealthBar healthBar;
    Component parentScript;
    int attackPower;

    private void Start() {
        //slime = transform.parent.GetComponent<Slime>();
        if (transform.parent.name.StartsWith("SlimeRed") || transform.parent.name.StartsWith("SlimeBlack")) {
            parentScript = transform.parent.GetComponent<Slime>();
            attackPower = parentScript.GetComponent<Slime>().attackPower;
        }
        else if (transform.parent.name.StartsWith("BatRed") || transform.parent.name.StartsWith("BatBlack")) {
            parentScript = transform.parent.GetComponent<Bat>();
            attackPower = parentScript.GetComponent<Bat>().attackPower;
        }
        healthBar = GameObject.Find("HealthPanel").GetComponent<HealthBar>();
    }

    private void FixedUpdate() {
        if (hurtBoxTimer < hurtBoxTimerMax) {
            hurtBoxTimer += Time.deltaTime;
        }
        if (hurtBoxTimer >= hurtBoxTimerMax) {
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            healthBar.TakeDamage(attackPower);
            parentScript.SendMessage("KnockBack");
            GetComponent<BoxCollider2D>().enabled = false;
            hurtBoxTimer = 0;
        }
    }
}