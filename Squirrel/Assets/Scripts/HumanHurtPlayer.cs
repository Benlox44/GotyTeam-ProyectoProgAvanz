using UnityEngine;

public class HumanHurtPlayer : MonoBehaviour {
    private Health health;
    [SerializeField] private int damageToGive;
    [SerializeField] private float attackCooldown;
    private Animator animator;
    private HumanHealth enemyHealth; 
    private float attackTimer;
    private bool isTouching;
    private bool canAttack;
    private Transform playerTransform;

    void Start() {
        health = FindObjectOfType<Health>();
        playerTransform = FindObjectOfType<PlayerController>().transform;
        canAttack = true;
        attackTimer = attackCooldown;
        animator = GetComponent<Animator>();
        enemyHealth = GetComponent<HumanHealth>(); 
    }

    void Update() {
        if (enemyHealth != null && (enemyHealth.isDead || enemyHealth.isTransitioningToRage)) return; 

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
        if (enemyHealth != null && (enemyHealth.isDead || enemyHealth.isTransitioningToRage)) return; 

        if (other.collider.tag == "Player" && canAttack) Attack();
    }

    private void OnCollisionStay2D(Collision2D other) {
        if (enemyHealth != null && (enemyHealth.isDead || enemyHealth.isTransitioningToRage)) return; 

        if (other.collider.tag == "Player") isTouching = true;
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (enemyHealth != null && (enemyHealth.isDead || enemyHealth.isTransitioningToRage)) return; 

        if (other.collider.tag == "Player") isTouching = false;
    }

    private void Attack() {
        canAttack = false;
        SetAttackDirection();
        health.HurtPlayer(damageToGive);
    }

    private void SetAttackDirection() {
        Vector2 attackDirection = GetAttackDirection();
        
        if (Mathf.Abs(attackDirection.x) > Mathf.Abs(attackDirection.y)) {
            if (attackDirection.x > 0) {
                animator.SetTrigger("AttackRight");
            } else {
                animator.SetTrigger("AttackLeft");
            }
        } else {
            if (attackDirection.y > 0) {
                animator.SetTrigger("AttackUp");
            } else {
                animator.SetTrigger("AttackDown");
            }
        }
    }

    private Vector2 GetAttackDirection() {
        Vector2 direction = playerTransform.position - transform.position;
        return direction.normalized;
    }

    public void IncreaseDamage(int multiplier) {
        damageToGive += multiplier;
    }
}
