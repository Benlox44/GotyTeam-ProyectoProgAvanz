using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject endGameUI; // Arrastra aquí tu Panel desde Unity
    public TMP_InputField playerNameInput; // Arrastra aquí tu InputField desde Unity
    public Button submitButton; // Arrastra aquí tu Button desde Unity
    public TextMeshProUGUI errorText; // Arrastra aquí tu Text para errores desde Unity

    void Start()
    {
        endGameUI.SetActive(false); 
        submitButton.onClick.AddListener(SubmitScore); 
        playerNameInput.onEndEdit.AddListener(SubmitScoreOnEnter); 
    }

    public void ShowEndGameUI()
    {
        Debug.Log("Mostrando UI de fin de juego");
        endGameUI.SetActive(true); 
        Time.timeScale = 0; 
    }

    public void SubmitScore()
    {
        string playerName = playerNameInput.text;
        if (string.IsNullOrEmpty(playerName))
        {
            errorText.text = "El nombre no puede estar vacío";
            return;
        }

        float score = FindObjectOfType<Score>().GetPuntos();
        StartCoroutine(FindObjectOfType<ScoreManager>().AddScore(playerName, (int)score, response =>
        {
            Debug.Log("Puntaje enviado: " + response);
            errorText.text = ""; 
            endGameUI.SetActive(false); 
            EndGame(); 
        }));
    }

    public void SubmitScoreOnEnter(string input)
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SubmitScore();
        }
    }

    private void EndGame()
    {
        
        Debug.Log("Finalizando el juego");
        Application.Quit(); 
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
        #endif
    }
}
