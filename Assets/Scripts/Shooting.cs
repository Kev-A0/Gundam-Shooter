using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This is a shooting class for the player.
/// When player hit spacebar, it will shoot a
/// projectile outwards in a up position.
/// 
/// Author: Brennen Chiu
/// Date: March 22, 2021; Revision: 1.0
/// </summary>
public class Shooting : MonoBehaviour
{
    public GameObject laser;
    public Transform shootingPoint;
    public Rigidbody2D rd2D;
    

    public float laserMovenment = 15f;

    // Update is called once per frame
    void Update() 
    {
        if(Input.GetKeyDown("space"))
        {
            Shoot();
        }
    }

    /// <summary>
    /// This method is to handle the projectile that is shooting 
    /// from the player.
    /// </summary>
    void Shoot()
    {
        // creating the bullet
        GameObject laserBullet = Instantiate(laser, shootingPoint.position, Quaternion.identity);
        rd2D = laserBullet.GetComponent<Rigidbody2D>();
        // making the bullet shoot
        rd2D.AddForce(shootingPoint.up * laserMovenment, ForceMode2D.Impulse);


    }
}
