using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.Networking;

public class RemoteStorage : MonoBehaviour
{

    public static RemoteStorage storage;
    private static string FirebaseBaseUrl = "https://us-central1-undercover-19bfd.cloudfunctions.net";
    private static string SaveScoreEndpoint = FirebaseBaseUrl + "/saveScore";
    private static string ReadScoreEndpoint = FirebaseBaseUrl + "/readScore";

    private void Awake()
    {
        if (storage == null)
        {
            storage = this;
            DontDestroyOnLoad(gameObject);
            
        }
        else if (storage != this)
        {
            Destroy(gameObject);
        }
    }

    public UnityWebRequest SaveScore(int score, string level)
    {
        var scoreData = new Score
        {
            userId = DataStorage.Data.userId,
            levelId = level,
            score = 300
        };
        
        var scoreJson = JsonUtility.ToJson(scoreData);
        byte[] body = new System.Text.UTF8Encoding().GetBytes(scoreJson);

        Debug.Log(scoreJson);

        UnityWebRequest request = new UnityWebRequest(SaveScoreEndpoint, "POST");
        request.downloadHandler = new DownloadHandlerBuffer();
        request.uploadHandler = new UploadHandlerRaw(body);

        request.SetRequestHeader("Content-Type", "application/json");
        StartCoroutine(SendPost(request));

        return request;
    }

    public Score GetScore(string level)
    {
        var scoreData = new Score
        {
            userId = DataStorage.Data.userId,
            levelId = level,
        };

        var scoreJson = JsonUtility.ToJson(scoreData);
        byte[] body = new System.Text.UTF8Encoding().GetBytes(scoreJson);

        Debug.Log(scoreJson);

        UnityWebRequest request = new UnityWebRequest(ReadScoreEndpoint, "POST");
        request.downloadHandler = new DownloadHandlerBuffer();
        request.uploadHandler = new UploadHandlerRaw(body);

        request.SetRequestHeader("Content-Type", "application/json");
        StartCoroutine(SendPost(request));

        return JsonUtility.FromJson<Score>(request.downloadHandler.text);
    }


    private IEnumerator SendPost(UnityWebRequest request)
    {
        yield return request.SendWebRequest();

        Debug.Log(request);
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log("HTTP ERROR: " + request.downloadHandler.text);
        } else
        {
            Debug.LogFormat("Request to {0} returned status code {1} with message {2}", request.url, request.responseCode, request.downloadHandler.text);
        }
    }

}

[Serializable]
public class Score
{
    public string userId;
    public string levelId;
    public int score;
}