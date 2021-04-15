using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using Firebase.Database;
using UnityEngine.UI;

/// <summary>
/// provide methods that displays all the scores
/// from the firebase Database. Displays the scores in the
/// Canvas.
/// 
/// Author: Kevin Lee
/// Date: April 14, 2021
/// Version: 1.0
/// </summary>
class LeaderboardControl : MonoBehaviour
{   
    /// <summary>
    /// Holds a reference to the Realtime database.
    /// </summary>
    private DatabaseReference db;

    /// <summary>
    /// Hold a reference to the user's score UI prefab.
    /// </summary>
    public GameObject scoreUI_Template;

    /// <summary>
    /// Holds a reference to the Canvas's content. This is where
    /// all the scores will be nested at.
    /// </summary>
    public GameObject leaderboardContent;

    private Dictionary<string, string> scoreList;


    void Awake()
    {
        db = FirebaseDatabase.DefaultInstance.RootReference;
        scoreList = new Dictionary<string, string>();
    }

    void Start()
    {
        ReadAllScores();

    }

    void Update()
    {
        //CreateScoreUI(scoreList);
        Debug.Log(scoreList.Count);
    }



    /// <summary>
    /// this method reads all the scores in the database.
    /// Source: https://firebase.google.com/docs/database/unity/retrieve-data
    /// Source: https://stackoverflow.com/questions/48860880/unity-firebase-database-retrieving-data
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    public async void ReadAllScores()
    {

        Dictionary<string, string> scores = new Dictionary<string, string>();
        await db.Child("users").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                throw new System.Exception("Database could read properly");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                        // Loop through the entire database.
                        foreach (DataSnapshot item in snapshot.Children)
                {
                    IDictionary dictUser = (IDictionary)item.Value;

                    scores.Add(dictUser["name"].ToString(), dictUser["points"].ToString());


                }
            }
        });


    }

    /// <summary>
    /// This method creates the score's UI element and append it 
    /// to the leaderboard.
    /// </summary>
    public void CreateScoreUI(Dictionary<string, string> scoreList)
    {

        foreach (KeyValuePair<string, string> item in scoreList)
        {
            Vector3 centerPos = new Vector3(0, 0, 0);

            // Create the user's score as a Unity UI prefab.
            GameObject userScore = Instantiate(scoreUI_Template, centerPos, Quaternion.identity);

            // Setting the name of the user.
            userScore.transform.GetChild(0).GetComponent<Text>().text = item.Key.ToString();

            // setting the value of the user.
            userScore.transform.GetChild(1).GetComponent<Text>().text = item.Value.ToString();

            // Set the score as a child of the leaderboard UI
            userScore.transform.SetParent(leaderboardContent.transform);

        }

    }


}

