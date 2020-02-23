using UnityEngine;

public class HurtBox : MonoBehaviour {
    [SerializeField] private float hurtBoxTimer = 1.0f;
    [SerializeField] private float hurtBoxTimerMax = 1.0f;
    [SerializeField] private HealthBar healthBar;
    private Slime slime;

    // Start is called before the first frame update
    private void Start() {
        slime = transform.parent.GetComponent<Slime>();
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
            healthBar.TakeDamage(slime.attackPower);
            slime.KnockBack();
            GetComponent<BoxCollider2D>().enabled = false;
            hurtBoxTimer = 0;
        }
    }
}