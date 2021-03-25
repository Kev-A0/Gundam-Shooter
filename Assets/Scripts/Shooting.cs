using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Shoot()
    {
        // creating the bullet
        GameObject laserBullet = Instantiate(laser, shootingPoint.position, Quaternion.identity);
        rd2D = laserBullet.GetComponent<Rigidbody2D>();
        // making the bullet shoot
        rd2D.AddForce(shootingPoint.up * laserMovenment, ForceMode2D.Impulse);


    }
}
