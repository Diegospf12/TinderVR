using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI usernameText;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] Image profileImage;
    // Start is called before the first frame update
    void Start()
    {
        usernameText.text = StaticData.usernameText;
        descriptionText.text = StaticData.DescriptionText;
        Texture2D texture = StaticData.profilePhoto;
        profileImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
