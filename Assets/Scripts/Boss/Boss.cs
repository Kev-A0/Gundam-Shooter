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
    private int switchLimit = 10;

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
        switchCounter++;
        if (switchCounter > switchLimit)
        {
            switchCounter = 0;
            currentPattern = UnityEngine.Random.Range(0, patternList.Count);
        }
        patternList[currentPattern]();


    }

    /// <summary>
    /// The allows the boss to take damage.
    /// </summary>
    /// <param name="damage"></param>
    public void takeDamage(int damage)
    {   
        health -= damage;
        GameObject.FindGameObjectWithTag("Boss Health Bar").GetComponent<Slider>().value = health;

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

    public override void Spawn(bool randomSpawn = false)
    {

        // spawn in the middle of the screen.
        Vector2 spawn_pos = Camera.main.ScreenToWorldPoint(new Vector2((Screen.width / 2), Screen.height));
        Instantiate(enemyObject, spawn_pos, Quaternion.identity);

        // Create the health bar.
        Vector3 healthBarPos = healthBar_prefab.GetComponent<RectTransform>().position;
        GameObject healthBar = Instantiate(healthBar_prefab, healthBarPos, Quaternion.identity);
        healthBar.GetComponent<Slider>().maxValue = health;
        healthBar.GetComponent<Slider>().value = health;

        healthBar.transform.SetParent(GameObject.Find("Canvas").transform, false);

    }

    public override void move()
    {
        this.direction.x = 0;
        this.direction.y = -1;
        Rigidbody2D rb2D = enemyObject.GetComponent<Rigidbody2D>();
        rb2D.gravityScale = 0;

        if (enemyObject.GetComponent<Transform>().position.y >= 4)
        {
            rb2D.MovePosition(rb2D.position + direction * moveSpeed * Time.fixedDeltaTime);
        }


        
    }


    void Awake()
    {
        patternList.Add(patterns.wave);
        patternList.Add(patterns.burst);
        patternList.Add(patterns.laser);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {

        // When this enemy collides with another object, check if it's a bullet.
        if (collision.gameObject.tag == "Bullet")
        {
            takeDamage(5);


            // Remove the bullet.
            Destroy(collision.gameObject);
        }
    }


    void Update()
    {
        // Add the amount of seconds that have passed since the last frame to get a consistant time.
        shoot_interval += Time.deltaTime;


        if (shoot_interval >= patterns.ShootTimeSpace)
        {
            shoot();
            shoot_interval = 0;
        }
    }


}
