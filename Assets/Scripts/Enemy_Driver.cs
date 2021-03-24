
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        EnemyEvent wave1 = new EnemyEvent();
        EnemyEvent wave2 = new EnemyEvent();
        EnemyEvent wave3 = new EnemyEvent();

        wave1.Add(enemy_list[0]);
        wave1.Add(enemy_list[0]);
        wave1.Add(enemy_list[0]);

        wave2.Add(enemy_list[0]);
        wave2.Add(enemy_list[1]);

        wave3.Add(enemy_list[2]);
        wave3.Add(enemy_list[1]);
        wave3.Add(enemy_list[1]);
        wave3.Add(enemy_list[2]);

        spawnList.Add(3, wave1);
        spawnList.Add(5, wave2);
        spawnList.Add(7, wave3);

    }


    void FixedUpdate()
    {
        float currentTime = timer.GetComponent<GameTimer>().time;

        if (spawnList.ContainsKey((int)currentTime))
        {
            spawnList[(int)currentTime].Notify();
            //spawnList.Remove((int)currentTime);
        }
        
        
    }
}
