using Oculus.Platform;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StaticActiveUsers : MonoBehaviour
{
    // list of all user active
    // show unique users active
    [SerializeField] public static List<User> ActiveUsers = new List<User> ();

    [SerializeField] TextMeshProUGUI username;
    [SerializeField] TextMeshProUGUI userdescription;
    [SerializeField] Texture2D userphoto;

    public static void AddUser(User user)
    {
        ActiveUsers.Add(user);
    }

    public static void RemoveUser(User user)
    {
        ActiveUsers.Remove(user);
    }
    // Start is called before the first frame update
    [System.Serializable]
    public class User
    {
        public int id;
        public string name;
        public string description;

    }
}
