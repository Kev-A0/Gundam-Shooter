using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;

/// <summary>
/// This class connects the the Firebase database and provide methods
/// that allow read and write to the database.
/// 
/// Author: Kevin Lee
/// Data: April 12, 2021
/// Version 1.0
/// </summary>
public class FirebaseAccess
{
    private DatabaseReference db;
    public string test;

    public FirebaseAccess()
    {
        this.db = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void WriteNewHighScore(string username, double points) {
        HighscoreUser user = new HighscoreUser(username, points);
        string json = JsonUtility.ToJson(user);
        db.Child("users").Child(username).SetRawJsonValueAsync(json);
    }


    /// <summary>
    /// this method displays all the scores in the database.
    /// Source: https://firebase.google.com/docs/database/unity/retrieve-data
    /// Source: https://stackoverflow.com/questions/48860880/unity-firebase-database-retrieving-data
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    public bool DisplayScores(double points)
    {
        
        db.Child("users").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                throw new System.Exception("Database could read properly");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot item in snapshot.Children)
                {
                    IDictionary dictUser = (IDictionary)item.Value;
                    //Debug.Log("" + dictUser["name"] + ": " + dictUser["points"]);
                }
            }
        });

        return false;
    }
            


}
