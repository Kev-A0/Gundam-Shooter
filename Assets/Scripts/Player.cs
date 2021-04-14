using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the player class which contains the 
/// death method for destorying the player object
/// once collide with enemy.
/// 
/// Author: Brennen Chiu
/// Date: March 23, 2021; Revision: 1.0
/// </summary>
public class Player : MonoBehaviour
{
    /// <summary>
    /// This is the PointsSystem. 
    /// Added: April 13, 2021
    /// </summary>
    public GameObject pointsSystem;

    public GameObject player;
    
    /// <summary>
    /// This method is to make the player disappear from 
    /// the screen. Displays the points menu
    /// Updated: April 14, 2021
    /// </summary>
    public void death()
    {
        Destroy(player);
        pointsSystem.GetComponent<PointsSystem>().displayPointsMenu(true);
    }
}
