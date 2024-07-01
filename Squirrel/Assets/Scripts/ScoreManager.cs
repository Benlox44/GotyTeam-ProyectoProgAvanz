using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class ScoreManager : MonoBehaviour
{
    // URL de la API para enviar puntajes
    private string apiUrl = "https://ucn-game-server.martux.cl/scores";
    // Token de autenticación
    private string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI2ZjE2Yzg3My00MDk5LTQzZjQtOTFkZC1iMTU2OTcxMDUzNDUiLCJrZXkiOiJWclNqdFEwam1uWXlQTFNYbm1mbXg1SldSIiwiaWF0IjoxNzE5NDYxNTMzLCJleHAiOjE3NTA5OTc1MzN9.m1SY77_IueP2TVDqVyuGFQW4z0XmqhAMlEbOjqa6v0U";

    public IEnumerator AddScore(string playerName, int score, Action<string> callback)
    {
        // Crea un objeto de puntaje en formato JSON
        ScoreData scoreData = new ScoreData { playerName = playerName, score = score };
        string jsonData = JsonUtility.ToJson(scoreData);

        // Crea una solicitud POST
        UnityWebRequest www = new UnityWebRequest(apiUrl, "POST");
        byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(jsonData);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("Authorization", "Bearer " + token);

        // Imprime el token para verificar
        Debug.Log("Token: " + token);

        // Envía la solicitud y espera la respuesta
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Puntaje enviado correctamente.");
            callback("Success");
        }
        else
        {
            Debug.Log("Error al enviar puntaje: " + www.error);
            callback("Error: " + www.error);
        }
    }
}

[System.Serializable]
public class ScoreData
{
    public string playerName;
    public int score;
}
