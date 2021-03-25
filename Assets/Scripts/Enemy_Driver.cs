
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Responsibility of this class is to use the EnemyEvent and Enemy Class.
/// Chooses what Enemy spawns at what time.
/// Author: Kevin Lee
/// Date: March 20, 2021; Revision: 1.0
/// </summary>
public class Enemy_Driver : MonoBehaviour
{   

    public Enemy[] enemy_list;
    public Dictionary<int, EnemyEvent> spawnList = new Dictionary<int, EnemyEvent>();
    public GameObject timer;

    /// <summary>
    /// Method from MonoBehaviour. The first one to be called when the Scene loads.
    /// This will be where all the enemies and their spawn times be created.
    /// </summary>
    private void Awake()
    {   
        // Create 3 sets of enemies that will spawn later.
        EnemyEvent wave1 = new EnemyEvent();
        EnemyEvent wave2 = new EnemyEvent();
        EnemyEvent wave3 = new EnemyEvent();

        // Begin adding the enemies to the object
        wave1.Add(enemy_list[0]);
        wave1.Add(enemy_list[0]);
        wave1.Add(enemy_list[0]);

        wave2.Add(enemy_list[0]);
        wave2.Add(enemy_list[1]);

        wave3.Add(enemy_list[2]);
        wave3.Add(enemy_list[1]);
        wave3.Add(enemy_list[1]);
        wave3.Add(enemy_list[2]);


        // Add the events to a Dictionary with a set amount of time.
        spawnList.Add(3, wave1);
        spawnList.Add(5, wave2);
        spawnList.Add(7, wave3);

    }


    void FixedUpdate()
    {   
        // Get the amount of time spent in the game.
        float currentTime = timer.GetComponent<GameTimer>().time;

        // Check if the amount of time matches any of the events in the Dicitonary.
        if (spawnList.ContainsKey((int)currentTime))
        {
            spawnList[(int)currentTime].Notify();
            spawnList.Remove((int)currentTime);
        }


    }
}
