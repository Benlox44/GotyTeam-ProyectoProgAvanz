using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePickup : MonoBehaviour
{
    [SerializeField] private float cantidadPuntos;
    [SerializeField] private Score puntajes;

    private void Awake() {
        puntajes = GameObject.FindObjectOfType<Score>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            puntajes.SumarPuntos(cantidadPuntos);
            Destroy(gameObject);
        }
    }
}
