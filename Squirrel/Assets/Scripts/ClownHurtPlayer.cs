using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownHurtPlayer : MonoBehaviour
{
    private Health health;
    [SerializeField] private int damageToGive;
    [SerializeField] private float attackCooldown;
    private float attackTimer;
    private bool isTouching;
    private bool canAttack;

    void Start() {
        health = FindObjectOfType<Health>();
        canAttack = true;
        attackTimer = attackCooldown;
    }

    void Update() {
        if (isTouching && canAttack) Attack();

        if (!canAttack) {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0f) {
                canAttack = true;
                attackTimer = attackCooldown;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.tag == "Player" && canAttack) Attack();
    }

    private void OnCollisionStay2D(Collision2D other) {
        if (other.collider.tag == "Player") isTouching = true;
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.collider.tag == "Player") isTouching = false;
    }

    private void Attack() {
        canAttack = false;
        health.HurtPlayer(damageToGive);
    }
}
