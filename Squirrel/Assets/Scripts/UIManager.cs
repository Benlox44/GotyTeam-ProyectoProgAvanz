using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject endGameUI; 
    public TMP_InputField playerNameInput; 
    public Button submitButton; 
    public TextMeshProUGUI errorText; 
    public TextMeshProUGUI scoresText; 

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

            
            StartCoroutine(FindObjectOfType<ScoreManager>().GetScores(DisplayScores));
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

    
    private void DisplayScores(List<ScoreData> scores)
    {
        if (scores != null)
        {
            scoresText.text = "Puntajes:\n";
            foreach (var score in scores)
            {
                scoresText.text += $"{score.playerName}: {score.score}\n";
            }
        }
        else
        {
            scoresText.text = "Error al obtener los puntajes.";
        }
    }
}
