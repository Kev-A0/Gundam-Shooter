using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// This class's responsibility is to notify all given Enemies
/// if an event occurs.
/// Author: Kevin Lee
/// Date: March 16, 2021; Revision: 1.1
/// </summary>
public class EnemyEvent
{   
    /// <summary>
    /// This will hold all the Enemies that will be notified later.
    /// </summary>
    private List<Enemy> enemyList;


    /// <summary>
    /// The Constructor for the Enemy's event.
    /// </summary>
    public EnemyEvent()
    {
        //Empty for now.
        enemyList = new List<Enemy>();
    }

    /// <summary>
    /// Add an enemy to the list of enemies.
    /// </summary>
    /// <param name="enemy"></param>
    public void Add(Enemy enemy)
    {
            
        enemyList.Add(enemy);
    }

    /// <summary>
    /// Remove a given enemy from the list of enemies.
    /// </summary>
    /// <param name="enemy"></param>
    public void Remove(Enemy enemy)
    {
        enemyList.Remove(enemy);
    }

    /// <summary>
    /// Notify all the enemies in the list to "spawn".
    /// </summary>
    public void Notify()
    {
        foreach (Enemy e in enemyList)
        {      
            e.Spawn(true);
        }
    }


    /// <summary>
    /// Tell all the enemies in this event to start moving.
    /// </summary>
    public void Notify_Move()
    {
        foreach (Enemy e in enemyList)
        {
            e.move();
        }
    }
}
