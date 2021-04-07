using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This is the boarder class for the player,
/// the player would not move out of screen.
/// 
/// Author: Brennen Chiu
/// Date: April 3th, 2021; Revision: 1.0
/// </summary>
public class PlayerBoader : MonoBehaviour
{
    /// <summary>
    /// The instance variable for the player boader, 
    /// which can be change in Unity.
    /// </summary>
    public float Player_Left_Boader;
    public float Player_Right_Boader;
    public float Player_Top_Boader;
    public float Player_Bottom_Boader;

    // Update is called once per frame
    /// <summary>
    /// This is to set the position of the player x, y boader.
    /// </summary>
    void Update()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, Player_Left_Boader, Player_Right_Boader),
            Mathf.Clamp(transform.position.y, Player_Bottom_Boader, Player_Top_Boader),
            transform.position.z);
    }
}
