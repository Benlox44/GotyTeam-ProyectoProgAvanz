using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody2D rigidBody;
    private Animator animator;
    [SerializeField] private float speed;

    private float attackTime;
    private float attackCounter;
    private bool isAttacking;

    [SerializeField] private float attackCooldown;
    private float cooldownCounter;

    private Item equippedItem;

    void Start() {
        attackTime = 0.5f;
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        HandleMovement();
        UpdateAnimatorParameters();
        HandleAttack();
    }

    void HandleMovement() {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(moveX, moveY).normalized * speed;
        rigidBody.velocity = movement;

        if (moveX != 0 || moveY != 0) {
            animator.SetFloat("lastMoveX", moveX);
            animator.SetFloat("lastMoveY", moveY);
        }
    }

    void UpdateAnimatorParameters() {
        animator.SetFloat("moveX", rigidBody.velocity.x);
        animator.SetFloat("moveY", rigidBody.velocity.y);
    }

    void HandleAttack() {
        if (isAttacking) {
            attackCounter -= Time.deltaTime;
            // rigidBody.velocity = Vector2.zero;
        }

        if (attackCounter <= 0) {
            animator.SetBool("isAttacking", false);
            isAttacking = false;
        }

        if (cooldownCounter > 0) {
            cooldownCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.K) && cooldownCounter <= 0) {
            attackCounter = attackTime;
            animator.SetBool("isAttacking", true);
            isAttacking = true;
            cooldownCounter = attackCooldown;
            UseItem();
        }
    }

     public void CollectItem(Item newItem) {
        equippedItem = newItem; // Asigna el ítem recogido al jugador
    }

    void UseItem() {
        if (equippedItem != null) {
            Debug.Log("Usando " + equippedItem.itemName);

            
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 1.0f); // Radio de ataque

            foreach (Collider2D enemy in hitEnemies) {
                if (enemy.CompareTag("Enemy")) {
                    
                    Vector2 knockbackDirection = (enemy.transform.position - transform.position).normalized;
                    enemy.GetComponent<EnemyHealth>().TakeDamage(20,knockbackDirection); 
                    Debug.Log("Golpeando enemigo: " + enemy.name);
                }
            }
        }
    }    

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1.0f); 
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Item")) {
            Item newItem = collision.GetComponent<Item>();
            if (newItem != null) {
                CollectItem(newItem);
                Destroy(collision.gameObject); 
            }
        }
    }
}