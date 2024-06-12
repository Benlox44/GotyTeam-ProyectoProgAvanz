using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map2AreaTransition : MonoBehaviour {
    private CameraController cam;
    [SerializeField] private Vector2 newMinPos;
    [SerializeField] private Vector2 newMaxPos;
    [SerializeField] private Vector3 movePlayer;
    private bool bossDefeated = false;

    void Start() {
        cam =  Camera.main.GetComponent<CameraController>();
        EnemyHealth.OnBossDeath += BossDefeated; // Suscribirse al evento de muerte del jefe
    }

    void OnDestroy() {
        EnemyHealth.OnBossDeath -= BossDefeated; // Desuscribirse del evento para evitar errores
    }

    private void BossDefeated() {
        bossDefeated = true; // Marcar que el jefe ha sido derrotado
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player" && bossDefeated) {
            cam.minPosition = newMinPos;
            cam.maxPosition = newMaxPos;
            other.transform.position += movePlayer;
        }   
    }
}
