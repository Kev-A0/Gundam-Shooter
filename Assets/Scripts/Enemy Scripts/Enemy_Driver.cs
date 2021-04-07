
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Responsibility of this class is to use the EnemyEvent and Enemy Class.
/// Chooses what Enemy spawns at what time.
/// Author: Kevin Lee
/// Date: March 20, 2021; Revision: 1.1
/// </summary>
public class Enemy_Driver : MonoBehaviour
{   

    public Enemy[] enemy_list;
    public Dictionary<int, EnemyEvent> spawnList = new Dictionary<int, EnemyEvent>();
    public GameObject timer;
    public Boss boss;


    /// <summary>
    /// This array holds the time in seconds. The numbers are used to spawn the enemies at a correct time.
    /// </summary>
    private int[] spawnTimeList = {3, 5, 10, 15, 20, 21, 23, 30, 35, 36, 39, 44, 46, 50, 55, 58 };

    /// <summary>
    /// Method from MonoBehaviour. The first one to be called when the Scene loads.
    /// This will be where all the enemies and their spawn times be created.
    /// </summary>
    private void Awake()
    {
        // Create 3 sets of enemies that will spawn later.
        EnemyEvent fast_enemy = new EnemyEvent();
        EnemyEvent slow_enemy = new EnemyEvent();
        EnemyEvent mix_enemy = new EnemyEvent();
        EnemyEvent boss_event = new EnemyEvent();

        try
        {
            fast_enemy.Add(enemy_list[1]);
            fast_enemy.Add(enemy_list[1]);
            fast_enemy.Add(enemy_list[1]);

            slow_enemy.Add(enemy_list[0]);
            slow_enemy.Add(enemy_list[0]);
            slow_enemy.Add(enemy_list[0]);

            mix_enemy.Add(enemy_list[0]);
            mix_enemy.Add(enemy_list[1]);
            mix_enemy.Add(enemy_list[1]);
            mix_enemy.Add(enemy_list[0]);

            boss_event.Add(boss);

        } catch (Exception ex)
        {
            Console.WriteLine(ex);
        }


        // Begin adding the enemies to the object
        EnemyEvent[] all_waves = { fast_enemy, slow_enemy, mix_enemy };

        // Add the events to a Dictionary with a set amount of time.
        for (int x = 0; x < spawnTimeList.Length; x++)
        {
            int randomIndex = UnityEngine.Random.Range(0, all_waves.Length);
            spawnList.Add(spawnTimeList[x], all_waves[randomIndex]);
        }

        // Set a time for the boss to spawn.
        spawnList.Add(60, boss_event);


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
