using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the player class which contains the 
/// death method for destorying the player object
/// once collide with enemy.
/// 
/// Author: Brennen Chiu
/// Date: March 23, 2021; Revision: 1.0
/// </summary>
public class Player : MonoBehaviour
{
    public GameObject player;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// This method is to make the player disappear from 
    /// the screen.
    /// </summary>
    public void death()
    {

       // anim.SetBool("isDead", true);
        //player.gameObject.SetActive(true);
        Destroy(player);
    }
}
