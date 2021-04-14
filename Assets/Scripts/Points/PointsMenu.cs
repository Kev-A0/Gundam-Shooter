using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This menu is responsible for displaying the points menu.
/// This menu displays when the user defeats the boss or the player loses.
/// Displays the current amount of points the user has gain.
/// 
/// Author: Kevin Lee
/// Data: April 12, 2021
/// Version 1.0
/// </summary>
public class PointsMenu : MonoBehaviour
{
    private bool displayPointsMenu;
    public GameObject pointMenuUI;
    public GameObject PointsSystem;
    public Text displayPoints;
    public InputField input_name;


    void Awake()
    {
        displayPointsMenu = false;
        pointMenuUI.SetActive(false);
    }
    

    public void displayMenu(bool displayMode)
    {
        displayPointsMenu = displayMode;
        pointMenuUI.SetActive(displayMode);
        displayPoints.text = "Points: " + PointsSystem.GetComponent<PointsSystem>().CurrentPoints;
    }

    /// <summary>
    /// This method restarts the current level.
    /// </summary>
    public void RestartGame()
    {   
        if (checkEmptyName())
        {
            displayMenu(false);
            SceneManager.LoadScene("SampleScene");
        }


        SaveScoreToDatabase();


    }

    /// <summary>
    /// This method returns the current player back to the 
    /// main menu.
    /// </summary>
    public void QuitGame()
    {
        
        if (checkEmptyName())
        {
            SceneManager.LoadScene("MainMenu");
        }


        SaveScoreToDatabase();
    }

    /// <summary>
    /// This method saves the user's score to the firebase database.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="points"></param>
    public void SaveScoreToDatabase()
    {

        double currentScore = PointsSystem.GetComponent<PointsSystem>().CurrentPoints;
        string user_name = input_name.text;

        FirebaseAccess firebase = new FirebaseAccess();

        firebase.WriteNewHighScore(user_name, currentScore);
    }
    
    /// <summary>
    /// Checks user input and make sure the user enters 
    /// a value in the text field.
    /// </summary>
    /// <returns></returns>
    public bool checkEmptyName()
    {
        string user_name = input_name.text.Trim();

        if (user_name.Length > 0)
        {
            return true;

        } else
        {
            return false;
        }

    }


}
