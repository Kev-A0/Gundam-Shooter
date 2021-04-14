using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class is responsible for add points to the points counter UI.
/// Whenever the player defeats an enemy or a boss, the points counter is incremented.
/// 
/// Author: Kevin Lee
/// Data: April 12, 2021
/// Version 1.0
/// </summary>
public class PointsSystem : MonoBehaviour
{   

    private int currentPoints = 0;
    public Text pointsUI;
    public GameObject pointsMenu;


    /// <summary>
    /// Update the current amount of points.
    /// Also update the Points UI.
    /// </summary>
    /// <param name="points"></param>
    public void addPoints(int points)
    {
        currentPoints += points;
        pointsUI.text = "Points: " + currentPoints;
    }
    
    /// <summary>
    /// Returns the current amount of points.
    /// </summary>
    public int CurrentPoints
    {
        get
        {
            return currentPoints;
        }
    }

    /// <summary>
    /// This method displays the points menu.
    /// </summary>
    /// <param name="displayMode"></param>
    public void displayPointsMenu(bool displayMode)
    {
        pointsMenu.GetComponent<PointsMenu>().displayMenu(displayMode);
    }
}
