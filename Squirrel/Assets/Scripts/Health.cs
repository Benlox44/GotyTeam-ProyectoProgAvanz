using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour {
    [SerializeField] private int numOfHearts;
    [SerializeField] private float timeToLoad;
    private bool isDead;

    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    private bool flashActive;
    [SerializeField] private float flashLength;
    private float flashCounter;

    private Animator animator;
    private SpriteRenderer playerSprite;
    private PlayerController playerMovement;

    void Start() {
        isDead = false;
        flashActive = false;
        animator = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerController>();
    }

    void Update() {
        UpdateHeartsDisplay();
        if (isDead) Reload();
        if (flashActive) FlashEffect();
    }

    public void HurtPlayer(int damageToGive) {
        if (isDead) return;
        numOfHearts -= damageToGive;
        flashActive = true;
        flashCounter = flashLength;
        if (numOfHearts <= 0 && !isDead) {
            isDead = true;
            animator.SetTrigger("Die");
            playerMovement.enabled = false;
        }
    }

    private void Reload() {
        timeToLoad -= Time.deltaTime;
        if (timeToLoad <= 0) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void UpdateHeartsDisplay() {
        for (int i = 0; i < hearts.Length; i++) {
            if (i < numOfHearts) {
                hearts[i].sprite = fullHeart;
            } else {
                hearts[i].sprite = emptyHeart;
            }
            hearts[i].enabled = true;
        }
    }

    private void FlashEffect() {
        float flashFrequency = 10f;
        float alpha = Mathf.Abs(Mathf.Sin(flashCounter * flashFrequency));
        playerSprite.color = new Color(1f, 1f, 1f, alpha);

        flashCounter -= Time.deltaTime;
        if (flashCounter <= 0) {
            playerSprite.color = new Color(1f, 1f, 1f, 1f);
            flashActive = false;
        }
    }
}