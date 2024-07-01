using UnityEngine;

public class HurtEnemy : MonoBehaviour {
    [SerializeField] private int damage;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null) {
                Vector2 knockbackDirection = other.transform.position - transform.position;
                enemyHealth.TakeDamage(damage, knockbackDirection);
            } else {
                HumanHealth HH = other.GetComponent<HumanHealth>();
                if (HH != null) {
                    Vector2 knockbackDirection = other.transform.position - transform.position;
                    HH.TakeDamage(damage, knockbackDirection);
                }
            }
        }
    }
}