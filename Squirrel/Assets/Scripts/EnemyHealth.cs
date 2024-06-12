using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private bool isBoss = false;
    [SerializeField] private int damageMultiplier = 2; 
    [SerializeField] private float speedBoost = 1f; 
    private int currentHealth;
    public bool isDead { get; private set; } 
    public bool isLowHealth { get; private set; } 
    private bool flashActive;
    private float flashLength;
    private float flashCounter;

    private SpriteRenderer enemySprite;
    private Rigidbody2D rb;
    private Animator animator;
    private EnemyController enemyController; 
    private HurtPlayer hurtPlayer; 

    [SerializeField] private GameObject healthPickupPrefab;

    public static event System.Action OnBossDeath; 

    void Start() {
        currentHealth = maxHealth;
        flashLength = 0.5f;
        enemySprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyController = GetComponent<EnemyController>();
        hurtPlayer = GetComponent<HurtPlayer>();
    }

    void Update() {
        if (flashActive) FlashEffect();
        CheckHealth();
    }

    private void CheckHealth() {
        if (!isLowHealth && currentHealth <= maxHealth * 0.5f) {
            isLowHealth = true;
            animator.SetBool("isLowHealth", true);
            if (enemyController != null) {
                enemyController.IncreaseSpeed(speedBoost); 
            }
            if (hurtPlayer != null) {
                hurtPlayer.IncreaseDamage(damageMultiplier); 
            }
        }
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
            OnBossDeath?.Invoke(); 
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

