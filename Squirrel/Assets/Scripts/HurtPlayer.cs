using UnityEngine;

public class HurtPlayer : MonoBehaviour {
    private Health health;
    [SerializeField] private int damageToGive;
    [SerializeField] private float attackCooldown;
    private Animator animator;
    private EnemyHealth enemyHealth; 
    private float attackTimer;
    private bool isTouching;
    private bool canAttack;

    void Start() {
        health = FindObjectOfType<Health>();
        canAttack = true;
        attackTimer = attackCooldown;
        animator = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>(); 
    }

    void Update() {
        if (enemyHealth != null && enemyHealth.isDead) return; 

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
        if (enemyHealth != null && enemyHealth.isDead) return; 

        if (other.collider.tag == "Player" && canAttack) Attack();
    }

    private void OnCollisionStay2D(Collision2D other) {
        if (enemyHealth != null && enemyHealth.isDead) return; 

        if (other.collider.tag == "Player") isTouching = true;
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (enemyHealth != null && enemyHealth.isDead) return; 

        if (other.collider.tag == "Player") isTouching = false;
    }

    private void Attack() {
        canAttack = false;
        animator.SetTrigger("Attack");
        health.HurtPlayer(damageToGive);
    }
}
