
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Responsibility of this class is to use the EnemyEvent and Enemy Class.
/// Chooses what Enemy spawns at what time.
/// Author: Kevin Lee
/// Date: March 20, 2021; Revision: 1.1
/// Updated: April 7, 2021
/// </summary>
public class Enemy_Driver : MonoBehaviour
{   
    /// <summary>
    /// This holds a list of all the enemies we can use.
    /// Enemies are used to form a set/group of enemies that will 
    /// spawn a certain time.
    /// </summary>
    public Enemy[] enemy_list;
    
    /// <summary>
    /// Uses the seconds as a key and a set of enemies as a value.
    /// The key is used to determine when to spawn the set of enemies.
    /// </summary>
    public Dictionary<int, EnemyEvent> spawnList = new Dictionary<int, EnemyEvent>();

    /// <summary>
    /// This holds a reference to the current game timer.
    /// </summary>
    public GameObject timer;

    /// <summary>
    /// This variables holds a reference to this current level's boss.
    /// Added on April 7, 2021
    /// </summary>
    public Boss boss;


    /// <summary>
    /// This array holds the time in seconds. The numbers are used to spawn the enemies at a correct time.
    /// Added on April 7, 2021
    /// </summary>
    private int[] spawnTimeList = {3, 5, 10, 15, 20, 21, 23, 31, 35, 36, 39, 44, 46, 50, 55, 58 };

    /// <summary>
    /// Method from MonoBehaviour. The first one to be called when the Scene loads.
    /// This will be where all the enemies and their spawn times be created.
    /// Updated: April 7, 2021
    /// </summary>
    private void Awake()
    {
        // Create 3 sets of enemies that will spawn later.
        EnemyEvent fast_enemy = new EnemyEvent();
        EnemyEvent slow_enemy = new EnemyEvent();
        EnemyEvent mix_enemy = new EnemyEvent();

        // Create a boss event.
        EnemyEvent boss_event = new EnemyEvent();

        // Incase the enemy list is empty.
        try
        {   
            // Group all the fast enemies into a set.
            fast_enemy.Add(enemy_list[1]);
            fast_enemy.Add(enemy_list[1]);
            fast_enemy.Add(enemy_list[1]);

            // Make a set of all slow enemies.
            slow_enemy.Add(enemy_list[0]);
            slow_enemy.Add(enemy_list[0]);
            slow_enemy.Add(enemy_list[0]);

            // Make a set that is a mix of fast and slow enemies.
            mix_enemy.Add(enemy_list[0]);
            mix_enemy.Add(enemy_list[1]);
            mix_enemy.Add(enemy_list[1]);
            mix_enemy.Add(enemy_list[0]);

            // Add a boss gameobject to a boss event.
            boss_event.Add(boss);

        } catch (Exception ex)
        {
            Console.WriteLine(ex);
        }


        // Begin adding the enemy events to the object
        EnemyEvent[] all_waves = { fast_enemy, slow_enemy, mix_enemy };

        // Add the events to a Dictionary with a set amount of time.
        for (int x = 0; x < spawnTimeList.Length; x++)
        {
            // Randomly choose enemy event and give it a timeslot.
            int randomIndex = UnityEngine.Random.Range(0, all_waves.Length);
            spawnList.Add(spawnTimeList[x], all_waves[randomIndex]);
        }

        // Set a time for the boss to spawn.
        spawnList.Add(60, boss_event);


    }

    /// <summary>
    /// Updates every frame.
    /// Keeps track of the current time. Checks if the current time matches any of the key (seconds) in the dictionary.
    /// </summary>
    void FixedUpdate()
    {   
        // Get the amount of time spent in the game.
        float currentTime = timer.GetComponent<GameTimer>().time;

        // Check if the amount of time matches any of the events in the Dicitonary.
        if (spawnList.ContainsKey((int)currentTime))
        {
            // Tell all the enemies in the list to spawn and remove them from the list.
            spawnList[(int)currentTime].Notify();
            spawnList.Remove((int)currentTime);
        }


    }
}
