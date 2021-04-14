using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class represents the enemies in this game.
/// Methods are provided that allow movement, attacking and spawning.
/// 
/// Author: Kevin Lee
/// Date: March 16, 2021; Revision: 1.1
/// Updated: April 7, 2021
/// </summary>
public class Enemy : MonoBehaviour, ISpawn
{
    /// <summary>
    /// This represents the enemies health.
    /// </summary>
    public int health;

    /// <summary>
    /// Represents the current direction this enemy is facing.
    /// </summary>
    public Vector2 direction;

    /// <summary>
    /// This holds the current movement speed of the enemy.
    /// </summary>
    public float moveSpeed;

    /// <summary>
    /// The name of the enemy.
    /// </summary>
    public string enemyName;

    /// <summary>
    /// Holds the game object / prefab of this enemy.
    /// </summary>
    public GameObject enemyObject;

    /// <summary>
    /// Holds the game object / prefab of the projectile that the enemy will shoot.
    /// </summary>
    public GameObject projectile;

    /// <summary>
    /// Holds a reference to where the projectile will spawn / appear.
    /// Used when the enemy shoots.
    /// </summary>
    public Transform shootingPoint;

    /// <summary>
    /// Holds the speed of the projectile.
    /// The default will be 10f.
    /// </summary>
    public float projectileSpeed = 10f;
    
    /// <summary>
    /// The time between each shot. After the prevous attack, the enemy
    /// has to wait this many seconds before shooting again.
    /// </summary>
    public float shoot_time = 0.5f;

    /// <summary>
    /// This is a counter that counts how many seconds have pass since the last projectile.
    /// Increments every seconds and resets to 0 when it gets bigger or equal to shoot_time.
    /// </summary>
    private float shoot_interval = 0;

    /// <summary>
    /// With a range between 0-10, holds the probablity of getting a powerup when this enemy
    /// dies.
    /// </summary>
    [SerializeField]
    private int dropRate;

    /// <summary>
    /// Holds a reference to a game object / prefab of a power up.
    /// This power up will have a chance to drop when this enemy dies.
    /// </summary>
    [SerializeField]
    private GameObject dropItem;
    [SerializeField]
    private GameObject specialDropItem;

    /// <summary>
    ///  This holds a reference to this game object's Rigidbody2D.
    ///  This is used to apply physics to the gameobject.
    ///  Updated: April 7, 2021
    /// </summary>
    public Rigidbody2D rb2D;

    /// <summary>
    /// This is responsible for making this enemy move in the level.
    /// Updated: April 7, 2021
    /// </summary>
    public virtual void move()
    {
        /** 
         * Move this downwards with the current position + -1 in the y direction * movement speed * FixedDeltaTime.
         * We use fixedDeltaTime to adjust for every frame. It keeps the movement smooth no matter the framerate.
         */
        rb2D.MovePosition(rb2D.position + direction * moveSpeed * Time.fixedDeltaTime);

        // Get the x, y positions of the camera relative to the screen.
        Vector2 out_pos = GameObject.Find("Main Camera").GetComponent<Camera>().WorldToScreenPoint(transform.position);

        // Use the renderer and screen position to check if the Enemy has left the screen.
        if (!GetComponent<Renderer>().isVisible && out_pos.y < 0)
        {   
            // Delete the Enemy when it's off screen.
            destroy();
            
        }


    }

    /// <summary>
    /// when called, the enemy will shoot a Projectile object at a certain direction.
    /// </summary>
    public virtual void shoot()
    {
        Vector3 downwards = shootingPoint.up * -1.0f;

        // creating the bullet
        GameObject bullet = Instantiate(projectile, shootingPoint.position, Quaternion.identity);

        // making the bullet shoot
        bullet.GetComponent<Rigidbody2D>().AddForce(downwards * projectileSpeed, ForceMode2D.Impulse);

    }

    /// <summary>
    /// This will destroy the current Enemy object.
    /// Usually called when the enemy loses all their health or goes off screen.
    /// </summary>
    public virtual void destroy()
    {   
        // Remove the object from the current screne.
        Destroy(enemyObject);

        // Use the dropRate to randomly determine if the Enemy drops a powerup.
        int dropChance = Random.Range(0, 10);
        if (dropChance >= dropRate)
        {   
            Instantiate(dropItem, GetComponent<Transform>().position, Quaternion.identity);
        }
        int specialDropChance = Random.Range(0, 50);
        if (specialDropChance <= dropRate)
        {
            Instantiate(specialDropItem, GetComponent<Transform>().position, Quaternion.identity);
        }
    }


    /// <summary>
    /// An overloaded version of the Spawn method.
    /// Provides the option to spawn the enemy in a random position on the man.
    /// </summary>
    /// <param name="x"></param>
    public virtual void Spawn(bool randomSpawn = false)
    {   

        if (randomSpawn)
        {
            // Get half of the sprites length to avoid spawning off camera.
            float spriteOffset = GetComponent<SpriteRenderer>().bounds.size.x / 2;
            // Convert the screen space x and y to world space x and y.
            Vector2 positionOnScreen = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

            // Add the offset to the range so the enemy doesn't spawn outside the camera.
            float left = -positionOnScreen.x + spriteOffset;
            float right = positionOnScreen.x - spriteOffset;

            // Get the random y position of the Enemy 
            Vector2 randomPos = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height));
            randomPos.x = Random.Range(left, right);

            // Spawn the enemy on the Scene.
            Instantiate(enemyObject, randomPos, Quaternion.identity);



        } else
        {   
            // if the option of spawning randomly is false, spawn in the middle of the screen.
            Vector2 spawn_pos = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height));
            Instantiate(enemyObject, spawn_pos, Quaternion.identity);
        }


        
    }

    /// <summary>
    /// Put the Enemy object into the current level.
    /// </summary>
    public virtual void Spawn()
    {   
        // Grab the middle position of the screen.
        Vector2 spawn_pos = new Vector2(0, Screen.height);

        // Convert the screen space x and y to world space x and y.
        spawn_pos = Camera.main.ScreenToWorldPoint(spawn_pos);
        // Spawn the Enemy in the Scene without rotation.
        Instantiate(enemyObject, spawn_pos, Quaternion.identity);
    }


    /// <summary>
    /// When the enemy Collides with another object.
    /// Should take damage when hit by a bullet.
    /// Updated: April 7, 2021
    /// </summary>
    /// <param name="collision"></param
    void OnTriggerEnter2D(Collider2D collision)
    {

        // When this enemy collides with another object, check if it's a bullet.
        if (collision.gameObject.tag == "Bullet")
        {
            // Remove this enemy.
            destroy();

            // Remove the bullet.
            Destroy(collision.gameObject);
        }
    }

    /// <summary>
    /// A method provided from MonoBehavior.
    /// This method gets called before the first frame.
    /// </summary>
    void Start()
    {
        // Change the direction of movement to south / downwards.
        this.direction.x = 0;
        this.direction.y = -1;
    }


    /// <summary>
    /// A method provided by MonoBehaviour
    /// This method gets called every frame.
    /// Mostly used for physics.
    /// </summary>
    void FixedUpdate()
    {

        // Move the enemy in the -y direction every frame.
        move();
    }

    /// <summary>
    /// A method provided by MonoBehaviour
    /// This method is called every frame.
    /// different because it's not used for physics.
    /// </summary>
    void Update()
    {
        // Add the amount of seconds that have passed since the last frame to get a consistant time.
        shoot_interval += Time.deltaTime;

        // If the time elapse if more or equal to the shoot_time, shoot a projectile and reset the interval to 0.
        if (shoot_interval >= shoot_time)
        {
            shoot();
            shoot_interval = 0;
        }
    }
}
