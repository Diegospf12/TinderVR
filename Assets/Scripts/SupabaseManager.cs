using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Text;

public class SupabaseManager : MonoBehaviour
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
                // Set online attr to TRUE
                string jsonBody = @"{ ""online"": ""true""}";
                byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);        
                url = $"{SUPABASE_URL}/rest/v1/users?online=true";               
                request = UnityWebRequest.Get(url);
                request.SetRequestHeader("apikey", SUPABASE_KEY);
                request.SetRequestHeader("Authorization", $"Bearer {SUPABASE_KEY}");
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Prefer", "request=minimal");
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                yield return request.SendWebRequest();

                User user = users[0];
                UnityWebRequest photosRequest = UnityWebRequestTexture.GetTexture(user.profilephoto);
                yield return photosRequest.SendWebRequest();
                loginCanvas.gameObject.SetActive(false);
                menuCanvas.gameObject.SetActive(true);
                usernameText.text = user.username;
                usernameText.color = Color.white;
                Texture2D texture = ((DownloadHandlerTexture)photosRequest.downloadHandler).texture;
                profileImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                StaticData.usernameText = user.username.ToString();
                StaticData.DescriptionText = user.description.ToString();
                StaticData.profilePhoto = texture;
                SceneManager.LoadScene(sceneName: "Scenes/Lobby");
            }
            else
            {
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