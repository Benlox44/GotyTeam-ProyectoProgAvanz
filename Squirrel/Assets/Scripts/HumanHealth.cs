using UnityEngine;
using UnityEngine.SceneManagement;

public class HumanHealth : MonoBehaviour {
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private bool isBoss = false;
    [SerializeField] private int damageMultiplier = 2; 
    [SerializeField] private float speedBoost = 1f; 
    [SerializeField] private bool isCompetitivo;
    private int currentHealth;
    public bool isDead { get; private set; } 
    public bool isLowHealth { get; private set; } 
    private bool flashActive;
    private float flashLength;
    private float flashCounter;

    private SpriteRenderer enemySprite;
    private Rigidbody2D rb;
    private Animator animator;
    private HumanController enemyController; 
    private HumanHurtPlayer hurtPlayer; 

    [SerializeField] private GameObject healthPickupPrefab;
    [SerializeField] private GameObject scorePickupPrefab; 

    public static event System.Action OnBossDeath; 
    public bool isTransitioningToRage { get; private set; }

    void Start() {
        currentHealth = maxHealth;
        flashLength = 0.5f;
        enemySprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyController = GetComponent<HumanController>();
        hurtPlayer = GetComponent<HumanHurtPlayer>();
    }

    void Update() {
        if (flashActive) FlashEffect();
        CheckHealth();
    }

    private void CheckHealth() {
        if (!isLowHealth && currentHealth <= maxHealth * 0.5f) {
            isLowHealth = true;
            isTransitioningToRage = true;
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
        if (isDead || isTransitioningToRage) return; 

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
            DropItems();
            Destroy(gameObject);
        }
    }

    public void OnDeathAnimationEnd() {
        DropItems();
        if (!isCompetitivo) SceneManager.LoadScene("Menu inicial");
        Destroy(gameObject);
    }

    private void DropItems() {
        if (healthPickupPrefab != null) {
            Instantiate(healthPickupPrefab, transform.position, Quaternion.identity);
        }
        if (scorePickupPrefab != null) {
            Instantiate(scorePickupPrefab, transform.position, Quaternion.identity);
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

    public void OnTransitionRageComplete()
    {
        isTransitioningToRage = false;
        animator.SetTrigger("FinTransicion");
    }

}
