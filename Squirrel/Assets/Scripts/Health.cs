using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private int numOfHearts;
    [SerializeField] private float timeToLoad;
    [SerializeField] private bool isCompetitiveMap = false;
    [SerializeField] private float gameTime = 60.0f;
    private float timer;
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

    private UIManager uiManager;

    void Start()
    {
        isDead = false;
        flashActive = false;
        animator = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerController>();

        if (isCompetitiveMap)
        {
            timer = gameTime; 
        }

        // Asegurarse de que el UIManager está asignado correctamente
        uiManager = FindObjectOfType<UIManager>();
        if (uiManager == null)
        {
            Debug.LogError("UIManager no encontrado en la escena.");
        }
    }

    void Update()
    {
        UpdateHeartsDisplay();
        if (isDead) Reload();
        if (flashActive) FlashEffect();

        if (isCompetitiveMap && timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                EndGame();
            }
        }
    }

    public void HurtPlayer(int damageToGive)
    {
        if (isDead) return;
        numOfHearts -= damageToGive;
        flashActive = true;
        flashCounter = flashLength;
        if (numOfHearts <= 0 && !isDead)
        {
            isDead = true;
            animator.SetTrigger("Die");
            playerMovement.enabled = false;
            EndGame(); // Llamar a EndGame cuando el jugador muere
        }
    }
    
    public void HealPlayer(int healthToGive)
    {
        numOfHearts += healthToGive;
        if (numOfHearts > hearts.Length) numOfHearts = hearts.Length;
        UpdateHeartsDisplay();
    }

    private void Reload()
    {
        timeToLoad -= Time.deltaTime;
        if (timeToLoad <= 0) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void UpdateHeartsDisplay()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].sprite = (i < numOfHearts) ? fullHeart : emptyHeart;
            hearts[i].enabled = true;
        }
    }

    private void FlashEffect()
    {
        float flashFrequency = 10f;
        float alpha = Mathf.Abs(Mathf.Sin(flashCounter * flashFrequency));
        playerSprite.color = new Color(1f, 1f, 1f, alpha);

        flashCounter -= Time.deltaTime;
        if (flashCounter <= 0)
        {
            playerSprite.color = new Color(1f, 1f, 1f, 1f);
            flashActive = false;
        }
    }

    private void EndGame()
    {   
        FindObjectOfType<UIManager>().ShowEndGameUI();
        Time.timeScale = 0;
        
    }
}
