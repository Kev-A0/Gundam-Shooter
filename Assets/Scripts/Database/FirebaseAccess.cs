using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using System.Threading.Tasks;
using UnityEngine.UI;

/// <summary>
/// This class connects the the Firebase database and provide methods
/// that allow read and write to the database.
/// 
/// Author: Kevin Lee
/// Data: April 12, 2021
/// Version 1.0
/// </summary>
public class FirebaseAccess : MonoBehaviour
{
    private DatabaseReference db;

    public GameObject scoreUI_Template;
    public GameObject leaderboardContent;

    public FirebaseAccess()
    {
        this.db = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void WriteNewHighScore(string username, double points) {
        HighscoreUser user = new HighscoreUser(username, points);
        string json = JsonUtility.ToJson(user);
        db.Child("users").Child(username).SetRawJsonValueAsync(json);
    }



            


}
