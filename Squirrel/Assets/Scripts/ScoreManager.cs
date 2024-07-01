using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class ScoreManager : MonoBehaviour
{
    // URL de la API para enviar puntajes
    private string apiUrl = "https://ucn-game-server.martux.cl/scores";
    // Token de autenticación proporcionado por tu profesor
    private string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI2ZjE2Yzg3My00MDk5LTQzZjQtOTFkZC1iMTU2OTcxMDUzNDUiLCJrZXkiOiJWclNqdFEwam1uWXlQTFNYbm1mbXg1SldSIiwiaWF0IjoxNzE5NDYxNTMzLCJleHAiOjE3NTA5OTc1MzN9.m1SY77_IueP2TVDqVyuGFQW4z0XmqhAMlEbOjqa6v0U";

    public IEnumerator AddScore(string playerName, int score, Action<string> callback)
    {
        // Validar el nombre del jugador y el puntaje
        if (string.IsNullOrEmpty(playerName) || playerName.Length > 10 || score < 1 || score > 999999999)
        {
            Debug.Log("Datos inválidos. Nombre del jugador: " + playerName + ", Puntaje: " + score);
            callback("Invalid data");
            yield break;
        }

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

        // Imprime el token y la URL para verificar
        Debug.Log("Token: " + token);
        Debug.Log("URL: " + apiUrl);
        Debug.Log("JSON Data: " + jsonData);

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

    public IEnumerator GetScores(Action<List<ScoreData>> callback)
    {
        UnityWebRequest www = UnityWebRequest.Get(apiUrl);
        www.SetRequestHeader("Authorization", "Bearer " + token);

        // Envía la solicitud y espera la respuesta
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Puntajes obtenidos correctamente: " + www.downloadHandler.text);
            List<ScoreData> scoreList = ProcessScores(www.downloadHandler.text);
            callback(scoreList);
        }
        else
        {
            Debug.Log("Error al obtener puntajes: " + www.error);
            callback(null);
        }
    }

    private List<ScoreData> ProcessScores(string json)
    {
        ScoreList scoreList = JsonUtility.FromJson<ScoreList>(json);
        return scoreList.data;
    }
}

[System.Serializable]
public class ScoreData
{
    public string playerName;
    public int score;
}

[System.Serializable]
public class ScoreList
{
    public List<ScoreData> data;
}
