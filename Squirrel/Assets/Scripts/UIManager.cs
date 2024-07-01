using System.Collections;
using System.Collections.Generic; // Asegúrate de que esta línea esté presente
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject endGameUI; // Panel de fin de juego
    public TMP_InputField playerNameInput; // Campo de entrada para el nombre del jugador
    public Button submitButton; // Botón para enviar el puntaje
    public TextMeshProUGUI errorText; // Texto para mostrar errores
    public TextMeshProUGUI scoresText; // Texto para mostrar los puntajes

    void Start()
    {
        endGameUI.SetActive(false); // Asegúrate de que el panel de fin de juego esté oculto al iniciar
        submitButton.onClick.AddListener(SubmitScore); // Añade el listener para el botón de envío
        playerNameInput.onEndEdit.AddListener(SubmitScoreOnEnter); // Añade el listener para la entrada de texto
    }

    // Método para mostrar la UI de fin de juego
    public void ShowEndGameUI()
    {
        Debug.Log("Mostrando UI de fin de juego");
        endGameUI.SetActive(true); // Muestra el panel de fin de juego
        Time.timeScale = 0; // Detiene el tiempo del juego
    }

    // Método para enviar el puntaje
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
            errorText.text = ""; // Limpia el texto de error
            endGameUI.SetActive(false); // Oculta el panel de fin de juego
            EndGame(); // Llama al método para finalizar el juego

            // Obtener y mostrar los puntajes después de enviar un puntaje
            StartCoroutine(FindObjectOfType<ScoreManager>().GetScores(DisplayScores));
        }));
    }

    // Método para enviar el puntaje cuando se presiona Enter
    public void SubmitScoreOnEnter(string input)
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SubmitScore();
        }
    }

    // Método para finalizar el juego
    private void EndGame()
    {
        Debug.Log("Finalizando el juego");
        Application.Quit(); // Cierra la aplicación
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Detiene el juego en el editor de Unity
        #endif
    }

    // Método para mostrar los puntajes obtenidos
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
