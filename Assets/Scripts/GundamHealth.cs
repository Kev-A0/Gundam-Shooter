using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            loseHealth(1);
            
        }
    }



}
