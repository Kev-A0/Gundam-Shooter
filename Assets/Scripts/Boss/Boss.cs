using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class represents the enemies in this game.
/// Methods are provided that allow movement, attacking and spawning.
/// 
/// Author: Kevin Lee
/// Date: April 7, 2021; Revision: 1.0
/// </summary>
public class Boss : Enemy
{   
    /// <summary>
    /// A reference to the projectilePattern Script / Object.
    /// This will be responsible for providing different attack patterns.
    /// </summary>
    public projectilePattern patterns;

    /// <summary>
    /// The number of times an attack pattern can be called before switch to a random one.
    /// </summary>
    [SerializeField]
    private int switchLimit = 5;

    /// <summary>
    /// Keeps track of how many times an attack has been used. After the attack has been
    /// used a certain amount of times, reset to 0 and pick another attack.
    /// should be around 10.
    /// </summary>
    [SerializeField]
    private int switchCounter = 0;

    /// <summary>
    /// This variable holds the index number that will be used in the PatternList
    /// </summary>
    [SerializeField]
    private int currentPattern = 0;

    /// <summary>
    /// Holds the methods that contain the attack patterns.
    /// Used to alter between attack patterns.
    /// </summary>
    public List<Action> patternList = new List<Action>();


    /// <summary>
    /// This holds a prefab that represents the health bar.
    /// </summary>
    public GameObject healthBar_prefab;

    /// <summary>
    /// Holds the amount of seconds that have pass, used to track when to shoot the 
    /// next projectile.
    /// </summary>
    private float shoot_interval = 0;

    /// <summary>
    /// The boss can shoot projectiles in different patterns.
    /// This gives the player a challenge.
    /// </summary>
    public override void shoot()
    {   
        // Keeps track of how many times the boss have shot.
        switchCounter++;

        // Switch the attack pattern when after a certain limit.
        if (switchCounter > switchLimit)
        {
            switchCounter = 0;
            int oldPattern = currentPattern;

            while (currentPattern == oldPattern)
            {
                currentPattern = UnityEngine.Random.Range(0, patternList.Count);
            }

        }

        // Choose an attack pattern from a list of patterns.
        patternList[currentPattern]();


    }

    /// <summary>
    /// The allows the boss to take damage.
    /// </summary>
    /// <param name="damage"></param>
    public void takeDamage(int damage)
    {   
        // Reduce the health by a certain amount and update the health bar UI.
        health -= damage;
        GameObject.FindGameObjectWithTag("Boss Health Bar").GetComponent<Slider>().value = health;

        // if the health reaches 0, distroy the boss and the health bar.
        if (health <= 0)
        {
            destroy();
            Destroy(GameObject.FindGameObjectWithTag("Boss Health Bar"));
        }
    }

    /// <summary>
    /// When the health reaches 0, the boss is distroyed.
    /// </summary>
    public override void destroy()
    {
        Destroy(enemyObject);
    }

    /// <summary>
    /// This is responsible for spawning the boss at the top of the level and remain 
    /// at the top of the level.
    /// </summary>
    /// <param name="randomSpawn"></param>
    public override void Spawn(bool randomSpawn = false)
    {

        // spawn at the top middle of the screen.
        Vector2 spawn_pos = Camera.main.ScreenToWorldPoint(new Vector2((Screen.width / 2), Screen.height));
        Instantiate(enemyObject, spawn_pos, Quaternion.identity);

        // Get the position of the healthbar and Create the health bar.
        Vector3 healthBarPos = healthBar_prefab.GetComponent<RectTransform>().position;
        GameObject healthBar = Instantiate(healthBar_prefab, healthBarPos, Quaternion.identity);

        // Set the max value and value of the health bar to the enemy boss's health.
        healthBar.GetComponent<Slider>().maxValue = health;
        healthBar.GetComponent<Slider>().value = health;

        // Nest the health bar in the current level's Canvas.
        healthBar.transform.SetParent(GameObject.Find("Canvas").transform, false);

    }

    /// <summary>
    /// This method if responsible for moving the Boss.
    /// This is an overrided version of the enemy.
    /// The boss moves downwards a certain distance and stops.
    /// </summary>
    public override void move()
    {
        // Makes sure the boss doesn't move too far downwards.
        if (enemyObject.GetComponent<Transform>().position.y >= 3.5)
        {
            rb2D.MovePosition(rb2D.position + direction * moveSpeed * Time.fixedDeltaTime);
        }


        
    }

    /// <summary>
    /// This gets called when the screen loads.
    /// Sets all important values first.
    /// </summary>
    void Awake()
    {   
        // Ensures the moving direction is downwards.
        this.direction.x = 0;
        this.direction.y = -1;

        // Add different attack patterns to the list of patterns.
        patternList.Add(patterns.wave);
        patternList.Add(patterns.burst);
        patternList.Add(patterns.laser);
    }

    /// <summary>
    /// This method is called when a projectile hits the boss.
    /// Uses the Collider2D component.
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter2D(Collider2D collision)
    {

        // When this enemy collides with another object, check if it's a bullet.
        if (collision.gameObject.tag == "Bullet")
        {   
            // The boss takes 5 damage.
            takeDamage(5);

            // Remove the bullet.
            Destroy(collision.gameObject);
        }
    }

    /// <summary>
    /// This method is provided by MonoBehaviour.
    /// This gets called every frame.
    /// </summary>
    void Update()
    {
        // Add the amount of seconds that have passed since the last frame to get a consistant time.
        shoot_interval += Time.deltaTime;

        // Checks if the current time since that projectile shot is valid.
        if (shoot_interval >= patterns.ShootTimeSpace)
        {
            // Shoots a projectile and reset the interval.
            shoot();
            shoot_interval = 0;
        }
    }


}
