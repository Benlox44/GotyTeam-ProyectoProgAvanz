using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    private bool flashActive;
    private float flashLength;
    private float flashCounter;

    private SpriteRenderer enemySprite;
    private Rigidbody2D rb;

    void Start() {
        currentHealth = maxHealth;
        flashLength = 0.5f;
        enemySprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (flashActive) FlashEffect();
    }


    public void TakeDamage(int damage, Vector2 knockbackDirection) {
        flashActive = true;
        flashCounter = flashLength;

        transform.position = new Vector2(transform.position.x + knockbackDirection.x, transform.position.y + knockbackDirection.y);

        currentHealth -= damage;
        if (currentHealth <= 0) {
            Die();
        }
    }

    private void Die() {
        Destroy(gameObject);
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