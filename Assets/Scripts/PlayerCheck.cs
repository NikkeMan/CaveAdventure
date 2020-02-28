using UnityEngine;

public class PlayerCheck : MonoBehaviour {
    public Component parentScript;

    private void Start() {
        if (transform.parent.name.StartsWith("SlimeRed") || transform.parent.name.StartsWith("SlimeBlack") || transform.parent.name.StartsWith("SlimeBoss")) {
            parentScript = transform.parent.GetComponent<Slime>();
        }
        else if (transform.parent.name.StartsWith("BatRed") || transform.parent.name.StartsWith("BatBlack")) {
            parentScript = transform.parent.GetComponent<Bat>();
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            parentScript.SendMessage("Attack");
        }
    }
}