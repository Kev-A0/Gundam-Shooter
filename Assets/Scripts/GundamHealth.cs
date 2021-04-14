using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This is the player health class in the game. 
/// This class allows the player to take damage from enemy
/// and add health when pick up a powerup, which represents
/// as an image.
/// 
/// Author: Brennen Chiu
/// Date: March 23, 2021; Revision: 1.0
/// </summary>
public class GundamHealth : MonoBehaviour
{
    public int health;
    public int numOfLives;

    public Image[] lives;
    public Sprite fullLives;
    public Sprite emptyLives;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    /// <summary>
    /// This method gets updated once per frame and set 
    /// health when it is bigger than number of lives, and 
    /// setting each sprite to display life image and no image.
    /// </summary>
    void Update()
    {
        if (health > numOfLives)
        {
            health = numOfLives;
        }



        for (int i = 0; i < lives.Length; i++)
        {
            if (i < health)
            {
                lives[i].sprite = fullLives;
            }
            else
            {
                lives[i].sprite = emptyLives;
            }

            if (i < numOfLives)
            {
                lives[i].enabled = true;
            }
            else
            {
                lives[i].enabled = false;
            }
        }
    }
    /// <summary>
    /// This is a method for lossing a life and 
    /// and destory player object when health is 0 
    /// and change the canvas to emptyLives sprite.
    /// </summary>
    /// <param name="heatsLost"></param>
    public void loseHealth(int heartsLost)
    {
        health -= heartsLost;

        if (health <= 0)
        {
            
            GetComponent<Player>().death();
            
            foreach (Image life in lives)
            {
                life.sprite = emptyLives;
            }

        }
    }

    /// <summary>
    /// When another game object's collider makes contact this collider.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            loseHealth(1);  
        }

        if (collision.gameObject.tag == "Enemy_Bullet")
        {
            loseHealth(1);
        }
    }
}
