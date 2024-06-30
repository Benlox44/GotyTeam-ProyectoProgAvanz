using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int healthToGive;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null) {
                playerHealth.HealPlayer(healthToGive);
                Destroy(gameObject);
            }
        }
    }
}