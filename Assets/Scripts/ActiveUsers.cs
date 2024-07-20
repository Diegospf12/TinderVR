using Meta.WitAi.Json;
using Oculus.Platform;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ActiveUsers : MonoBehaviour
{
    private const string SUPABASE_URL = "https://maswuwwgqsiadeecdytp.supabase.co";
    private const string SUPABASE_KEY = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Im1hc3d1d3dncXNpYWRlZWNkeXRwIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MTg1Njk4MzYsImV4cCI6MjAzNDE0NTgzNn0.vIGnyUOJarJFZ-s4gPCdlbEdqWiGjx_hVWx53vGnd5s";

    [SerializeField] InputField UsernameField;
    [SerializeField] InputField PasswordField;
    [SerializeField] TextMeshProUGUI usernameText;
    [SerializeField] TextMeshProUGUI messageText;
    [SerializeField] Canvas loginCanvas;
    [SerializeField] Canvas menuCanvas;
    [SerializeField] Image profileImage;
    string url = $"{SUPABASE_URL}/rest/v1/useractives?select=*";
    void Start()
    {
        StartCoroutine(ExecuteQueryAsync());
        //List<int> activeUserIds = await ExecuteQueryAsync<List<int>>(activeUsersQuery);
    }

    private IEnumerator ExecuteQueryAsync()
    {
        string sqlQuery = "select * from \"users\" where id in ( select user_id from \"useractives\" )";
        string url = $"{SUPABASE_URL}/rest/v1/rpc/query";

        UnityWebRequest request = UnityWebRequest.PostWwwForm(url, "POST");
        request.SetRequestHeader("apikey", SUPABASE_KEY);
        request.SetRequestHeader("Authorization", $"Bearer {SUPABASE_KEY}");
        request.SetRequestHeader("Content-Type", "application/json");

        string jsonBody = JsonUtility.ToJson(new { query = sqlQuery });
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Error: {request.error}");
        }
        else
        {
            string responseBody = request.downloadHandler.text;

            //T result = JsonConvert.DeserializeObject<T>(responseBody); 
            //callback?.Invoke(result);
        }
    }
}
