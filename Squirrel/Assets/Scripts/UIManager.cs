using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject endGameUI; 
    public TMP_InputField playerNameInput; 
    public Button submitButton; 
    public TextMeshProUGUI errorText; 
    public TextMeshProUGUI scoresText; 
    [SerializeField] bool wantDelete;
    [SerializeField] bool wantShow;

    void Start()
    {
        endGameUI.SetActive(false); 

        if (wantDelete) {
        StartCoroutine(FindObjectOfType<ScoreManager>().DeleteAllScores(response =>
        {
            // Manejar la respuesta aquí
            Debug.Log("Respuesta de eliminación de puntajes: " + response);
        }));
        }
    }

    
    public void ShowEndGameUI()
    {
        Debug.Log("Mostrando UI de fin de juego");
        endGameUI.SetActive(true); 
        Time.timeScale = 0; 
        if (wantShow) StartCoroutine(FindObjectOfType<ScoreManager>().GetScores(DisplayScores));
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
        SceneManager.LoadScene("Menu inicial");
    }

    
    private void DisplayScores(List<ScoreData> scores)
    {
        if (scores != null)
    {
        scoresText.text = "TOP 10 PUNTAJES:\n";
        
        // Limitar la cantidad de puntajes mostrados a los primeros 10
        int count = Mathf.Min(scores.Count, 10);
        
        for (int i = 0; i < count; i++)
        {
            scoresText.text += $"{scores[i].playerName}: {scores[i].score}\n";
        }
    }
        else
        {
            scoresText.text = "Error al obtener los puntajes.";
        }
    }
}