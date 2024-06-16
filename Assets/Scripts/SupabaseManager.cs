using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SupabaseManager : MonoBehaviour
{
    private const string SUPABASE_URL = "https://maswuwwgqsiadeecdytp.supabase.co";
    private const string SUPABASE_KEY = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Im1hc3d1d3dncXNpYWRlZWNkeXRwIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MTg1Njk4MzYsImV4cCI6MjAzNDE0NTgzNn0.vIGnyUOJarJFZ-s4gPCdlbEdqWiGjx_hVWx53vGnd5s";

    [SerializeField] TMP_InputField UsernameField;
    [SerializeField] TMP_InputField PasswordField;
    [SerializeField] TextMeshProUGUI usernameText;
    [SerializeField] TextMeshProUGUI messageText;

    public void ValidateLogin()
    {
        string username = UsernameField.text;
        string password = PasswordField.text;

        StartCoroutine(Login(username, password));
    }

    public IEnumerator Login(string username, string password)
    {
        string url = $"{SUPABASE_URL}/rest/v1/users?username=eq.{username}&password=eq.{password}";

        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("apikey", SUPABASE_KEY);
        request.SetRequestHeader("Authorization", $"Bearer {SUPABASE_KEY}");
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);

            // Parse the response JSON to extract user data
            User[] users = JsonHelper.FromJson<User>(request.downloadHandler.text);

            if (users.Length > 0)
            {
                User user = users[0];
                Debug.Log($"User: {user.username}, Description: {user.description}");
                messageText.text = $"Welcome!";
                messageText.color = Color.green;
                usernameText.text = user.username;
                usernameText.color = Color.blue;
            }
            else
            {
                Debug.Log("Invalid username or password.");
                messageText.text = "Usuario o contraseña inválidos";
                messageText.color = Color.red;
            }
        }
    }


    [System.Serializable]
    public class User
    {
        public string username;
        public string password;
        public string description;
        public string profilephoto;
    }
}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        string newJson = "{ \"array\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.array;
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }
}
