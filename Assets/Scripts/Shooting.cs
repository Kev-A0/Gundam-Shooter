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
    public GameObject bullet;
    public Transform shootingPoint;
    public Rigidbody2D rd2D;
    public GameObject laser_prefab;

    public float bulletSpeed = 13f;
    public float laserSpeed = 15f;

    private float timeLeft = 1.0f;

    // Update is called once per frame
    void Update() 
    {
        if (Input.GetKeyDown("space"))
        {
            Shoot();
        }

        // This is the special shot part
        if (GetComponent<GundamSpecial>().specialAvaliable == 0)
        {
            if (Input.GetKey("e"))
            {
                Debug.Log("e is disable");
            }
        }
        else if (GetComponent<GundamSpecial>().specialAvaliable == 1)
        {
            if (Input.GetKey("e"))
            {
                timeLeft -= Time.deltaTime;
                SpecialMove();

                // if timer goes to zero, it will disable the special features
                if (timeLeft < 0)
                {
                    // here is to minus the special avaliablilty
                    GetComponent<GundamSpecial>().specialAvaliable -= GetComponent<GundamSpecial>().specialAvaliable;
                    timeLeft = 1.0f;
                }
            }
        }
    }

    /// <summary>
    /// This method is to handle the projectile that is shooting 
    /// from the player.
    /// </summary>
    void Shoot()
    {
        // creating the bullet
        GameObject projectile = Instantiate(bullet, shootingPoint.position, Quaternion.identity);
        rd2D = projectile.GetComponent<Rigidbody2D>();
        // making the bullet shoot
        rd2D.AddForce(shootingPoint.up * bulletSpeed, ForceMode2D.Impulse);
    }

    /// <summary>
    /// When the player presses the "e" button, the player shoots a massive laser.
    /// </summary>
    void SpecialMove()
    {
        GameObject laser = Instantiate(laser_prefab, shootingPoint.position, Quaternion.identity);
        laser.GetComponent<Rigidbody2D>().AddForce(shootingPoint.up * laserSpeed, ForceMode2D.Impulse);
    }
}
