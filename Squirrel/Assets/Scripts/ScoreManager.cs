using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ScoreManager : MonoBehaviour
{
    private string baseUrl = "https://ucn-game-server.martux.cl/scores";
    private string authToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI2ZjE2Yzg3My00MDk5LTQzZjQtOTFkZC1iMTU2OTcxMDUzNDUiLCJrZXkiOiJWclNqdFEwam1uWXlQTFNYbm1mbXg1SldSIiwiaWF0IjoxNzE5NDYxNTMzLCJleHAiOjE3NTA5OTc1MzN9.m1SY77_IueP2TVDqVyuGFQW4z0XmqhAMlEbOjqa6v0U";

    public IEnumerator GetScores(System.Action<string> callback)
    {
        string url = baseUrl;

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            www.SetRequestHeader("Authorization", "Bearer " + authToken);

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error al obtener puntajes: " + www.error);
            }
            else
            {
                callback?.Invoke(www.downloadHandler.text);
            }
        }
    }

    public IEnumerator AddScore(string playerName, int score, System.Action<string> callback)
    {
        string url = baseUrl;
        string jsonBody = "{\"playerName\": \"" + playerName + "\", \"score\": " + score + "}";
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);

        using (UnityWebRequest www = new UnityWebRequest(url, "POST"))
        {
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Authorization", "Bearer " + authToken);
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error al agregar puntaje: " + www.error);
            }
            else
            {
                callback?.Invoke(www.downloadHandler.text);
            }
        }
    }

    public IEnumerator DeleteAllScores(System.Action<string> callback)
    {
        string url = baseUrl;

        using (UnityWebRequest www = UnityWebRequest.Delete(url))
        {
            www.SetRequestHeader("Authorization", "Bearer " + authToken);

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error al eliminar puntajes: " + www.error);
            }
            else
            {
                callback?.Invoke(www.downloadHandler.text);
            }
        }
    }
}
