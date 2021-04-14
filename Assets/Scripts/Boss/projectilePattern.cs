using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible for providing different attack patterns to a 
/// game object. These attack patterns rely on multiple spawn points for projectiles.
/// 
/// Author: Kevin Lee
/// Date: April 7, 2021; Revision: 1.0
/// </summary>
public class projectilePattern : MonoBehaviour
{
    /// <summary>
    /// The holds the time that passes every seconds. The time between each projectile
    /// will be determined by this value.
    /// </summary>
    [SerializeField]
    private float shootTimeSpace = 2.5f;

    /// <summary>
    /// A property for the ShootTimeSpace.
    /// </summary>
    public float ShootTimeSpace
    {
        get { return shootTimeSpace; }
        set { shootTimeSpace = value; }
    }

    /// <summary>
    /// This will hold a reference to a projectile gameobject.
    /// </summary>
    [SerializeField]
    private GameObject projectile;

    /// <summary>
    /// This will hold a reference to a laser projectile gameobject.
    /// </summary>
    [SerializeField]
    private GameObject laserProjectile;

    /// <summary>
    /// This array stores the location of where the projectiles / bullets will spawn.
    /// </summary>
    public Transform[] shootingPoints;

    /// <summary>
    /// This holds the speed of the projectile.
    /// </summary>
    public float projectileSpeed;

    /// <summary>
    /// Starting from the left spawn point, keeps track on which one has been used
    /// before resetting to 0.
    /// </summary>
    private int waveCounter = 0;

    /// <summary>
    /// Go through each projectile spawn point and spawn a projectile.
    /// </summary>
    public void wave()
    {   
        // Set the time between each shot and projectile speed.
        shootTimeSpace = 0.5f;
        projectileSpeed = 0.5f;

        // Get a reference to one of the spawn points for the bullet.
        Transform point = shootingPoints[waveCounter];

        // Set the travel direction for the bullet.
        Vector3 bulletPath = point.position;
        bulletPath.y = bulletPath.y * -1f;

        // Create the bullet with the gameobject, direction and no rotation.
        GameObject bullet = Instantiate(projectile, point.position, Quaternion.identity);

        // Give the bullet some force and speed downwards
        bullet.GetComponent<Rigidbody2D>().AddForce(bulletPath * projectileSpeed, ForceMode2D.Impulse);

        // Increment so that the next projectile will spawn at a different spawn point.
        waveCounter++;
        
        // If we reach the last spawn point, begin at the first spawn point.
        if (waveCounter >= shootingPoints.Length)
        {
            waveCounter = 0;
        }
    }

    /// <summary>
    ///  For all the spawn points, spawn a projectile at the same time.
    ///  All the bullets should be created and travel at the same time.
    /// </summary>
    public void burst()
    {
        // Set the time between each shot and projectile speed.
        shootTimeSpace = 1f;
        projectileSpeed = 0.5f;

        // Go through all the spawn points and spawn a bullet. 
        foreach (Transform point in shootingPoints)
        {   
            // Set the travel direction downwards.
            Vector3 bulletPath = point.position;
            bulletPath.y = bulletPath.y * -1f;

            // Create the bullet with the gameobject, direction and no rotation.
            GameObject bullet = Instantiate(projectile, point.position, Quaternion.identity);

            // Give the bullet some force and speed downwards
            bullet.GetComponent<Rigidbody2D>().AddForce(bulletPath * projectileSpeed, ForceMode2D.Impulse);


        }

 
    }

    /// <summary>
    /// this shoots a big laser for a short period of time.
    /// </summary>
    public void laser()
    {
        // Set the time between each shot and projectile speed. Should be fast.
        shootTimeSpace = 0.05f;
        projectileSpeed = 3f;

        // Get the middle spawn point.
        Transform point = shootingPoints[2];

        // Set the direction of the of the laser. downwards.
        Vector3 bulletPath = point.position;
        bulletPath.y = bulletPath.y * -1f;

        // Create the laser and give it a downward force.
        GameObject bullet = Instantiate(laserProjectile, point.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().AddForce(bulletPath * projectileSpeed, ForceMode2D.Impulse);


    }
}
