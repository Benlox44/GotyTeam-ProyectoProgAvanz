using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int healthToGive;
    [SerializeField] private float cantidadPuntos;
    [SerializeField] private Puntajes puntajes;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null) {
                playerHealth.HealPlayer(healthToGive);
                Destroy(gameObject); 
                puntajes.SumarPuntos(cantidadPuntos);
                
            }
        }
    }
}


