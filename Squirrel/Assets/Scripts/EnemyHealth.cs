using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private bool isBoss = false;
    private int currentHealth;
    public bool isDead { get; private set; } 
    private bool flashActive;
    private float flashLength;
    private float flashCounter;

    private SpriteRenderer enemySprite;
    private Rigidbody2D rb;
    private Animator animator;

    [SerializeField] private GameObject healthPickupPrefab;

    void Start() {
        currentHealth = maxHealth;
        flashLength = 0.5f;
        enemySprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        if (flashActive) FlashEffect();
    }

    public void TakeDamage(int damage, Vector2 knockbackDirection) {
        if (isDead) return; 

        flashActive = true;
        flashCounter = flashLength;

        transform.position = new Vector2(transform.position.x + knockbackDirection.x, transform.position.y + knockbackDirection.y);

        currentHealth -= damage;
        if (currentHealth <= 0) {
            Die();
        }
    }

    private void Die() {
        isDead = true; 

        if (isBoss) {
            animator.SetTrigger("Die");
        } else {
            DropHealthItem();
            Destroy(gameObject);
        }
    }

    public void OnDeathAnimationEnd() {
        DropHealthItem();
        Destroy(gameObject);
    }

    private void DropHealthItem() {
        if (healthPickupPrefab != null) {
            Instantiate(healthPickupPrefab, transform.position, Quaternion.identity);
        }
    }

    private void FlashEffect() {
        float flashFrequency = 10f;
        float alpha = Mathf.Abs(Mathf.Sin(flashCounter * flashFrequency));
        enemySprite.color = new Color(1f, 1f, 1f, alpha);

        flashCounter -= Time.deltaTime;
        if (flashCounter <= 0) {
            enemySprite.color = new Color(1f, 1f, 1f, 1f);
            flashActive = false;
        }
    }
}
