using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class represents the enemies in this game.
/// Methods are provided that allow movement, attacking and spawning.
/// 
/// Author: Kevin Lee
/// Date: March 16, 2021; Revision: 1.0
/// </summary>
public class Enemy : MonoBehaviour, ISpawn
{
    public int health;
    public Vector2 direction;
    public float moveSpeed;
    public int attackPower;
    public string enemyName;
    public GameObject enemyObject;

    [SerializeField]
    private int dropRate;
    [SerializeField]
    private GameObject dropItem;

    /// <summary>
    /// This is responsible for making this enemy move in the level.
    /// </summary>
    public void move()
    {
        this.direction.x = 0;
        this.direction.y = -1;
        Rigidbody2D rb2D = enemyObject.GetComponent<Rigidbody2D>();
        rb2D.gravityScale = 0;


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
    public void shoot()
    {
        // UNDONE: Implement feature in next iteration.

    }

    /// <summary>
    /// This will destroy the current Enemy object.
    /// Usually called when the enemy loses all their health or goes off screen.
    /// </summary>
    public void destroy()
    {   
        // Remove the object from the current screne.
        Destroy(enemyObject);

        // Use the dropRate to randomly determine if the Enemy drops a powerup.
        int dropChance = Random.Range(0, 10);
        if (dropChance >= dropRate)
        {   
            Instantiate(dropItem, GetComponent<Transform>().position, Quaternion.identity);
        }
    }

    /// <summary>
    /// When the enemy takes damage, this will subtract the damage from it's health.
    /// </summary>
    /// <param name="damage"></param>
    public void takeDamage(int damage)
    {
        //TODO: Reduce the Enemy's health by a certain amount.
    }

    /// <summary>
    /// An overloaded version of the Spawn method.
    /// Provides the option to spawn the enemy in a random position on the man.
    /// </summary>
    /// <param name="x"></param>
    public void Spawn(bool randomSpawn = false)
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
    public void Spawn()
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
    /// </summary>
    /// <param name="collision"></param>
    public void OnCollisionEnter2D(Collision2D collision)
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


    void FixedUpdate()
    {   
        // Move the enemy in the -y direction every frame.
        move();
    }
}
