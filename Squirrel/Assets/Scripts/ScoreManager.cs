// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Networking;

// public class ScoreManager : MonoBehaviour
// {
//     private string baseUrl = "https://game.example.com/scores";
//     private string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwiaWF0IjoxNTE2MjM5MDIyfQ.L8i6g3PfcHlioHCCPURC9pmXT7gdJpx3kOoyAfNUwCc";

//     // Método para obtener los puntajes
//     public void GetScores()
//     {
//         StartCoroutine(GetScoresCoroutine());
//     }

//     private IEnumerator GetScoresCoroutine()
//     {
//         UnityWebRequest www = UnityWebRequest.Get(baseUrl);
//         www.SetRequestHeader("Authorization", "Bearer " + token);
//         yield return www.SendWebRequest();

//         if (www.result == UnityWebRequest.Result.Success)
//         {
//             Debug.Log(www.downloadHandler.text);
//             // Procesa la respuesta JSON aquí
//         }
//         else
//         {
//             Debug.LogError(www.error);
//         }
//     }

//     // Método para agregar un nuevo puntaje
//     public void AddScore(string playerName, int score)
//     {
//         StartCoroutine(AddScoreCoroutine(playerName, score));
//     }

//     private IEnumerator AddScoreCoroutine(string playerName, int score)
//     {
//         var jsonData = new Dictionary<string, object>
//         {
//             { "playerName", playerName },
//             { "score", score }
//         };

//         string json = JsonUtility.ToJson(jsonData);

//         UnityWebRequest www = UnityWebRequest.Post(baseUrl, json);
//         www.SetRequestHeader("Authorization", "Bearer " + token);
//         www.SetRequestHeader("Content-Type", "application/json");
//         www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(json));
//         www.downloadHandler = new DownloadHandlerBuffer();
        
//         yield return www.SendWebRequest();

//         if (www.result == UnityWebRequest.Result.Success)
//         {
//             Debug.Log(www.downloadHandler.text);
//             // Procesa la respuesta JSON aquí
//         }
//         else
//         {
//             Debug.LogError(www.error);
//         }
//     }

//     // Método para eliminar todos los puntajes
//     public void DeleteScores()
//     {
//         StartCoroutine(DeleteScoresCoroutine());
//     }

//     private IEnumerator DeleteScoresCoroutine()
//     {
//         UnityWebRequest www = UnityWebRequest.Delete(baseUrl);
//         www.SetRequestHeader("Authorization", "Bearer " + token);
//         yield return www.SendWebRequest();

//         if (www.result == UnityWebRequest.Result.Success)
//         {
//             Debug.Log(www.downloadHandler.text);
//         }
//         else
//         {
//             Debug.LogError(www.error);
//         }
//     }
// }