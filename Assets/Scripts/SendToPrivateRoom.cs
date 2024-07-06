using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SendToPrivateRoom : MonoBehaviour
{
    // Start is called before the first frame update
    private string Enviroment = "picnic";
    [SerializeField] TextMeshProUGUI SelectedEnv;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendToRoom()
    {
        Enviroment = SelectedEnv.text.ToString().ToLower();
        if (Enviroment == "picnic")
        {
            SceneManager.LoadScene(sceneName: "Scenes/PicnicEnviroment");
        }
        else if (Enviroment == "camping")
        {
            SceneManager.LoadScene(sceneName: "Scenes/CampingEnviroment");
        }
    }
}
