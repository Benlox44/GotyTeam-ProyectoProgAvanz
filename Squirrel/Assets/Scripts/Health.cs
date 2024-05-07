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

    void Start() {
        isDead = false;
    }

    void Update() {
        UpdateHeartsDisplay();
        if (isDead) Reload();
    }

    public void HurtPlayer(int damageToGive) {
        numOfHearts -= damageToGive;
        if (numOfHearts <= 0) isDead = true;
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
}