using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectilePattern : MonoBehaviour
{
    /// <summary>
    /// The holds the time that passes every seconds. The time between each projectile
    /// will be determined by this value.
    /// </summary>
    [SerializeField]
    private float shootTimeSpace = 2.5f;

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
        shootTimeSpace = 0.5f;
        projectileSpeed = 0.5f;

        Transform point = shootingPoints[waveCounter];

        Vector3 bulletPath = point.position;
        bulletPath.y = bulletPath.y * -1f;

        GameObject bullet = Instantiate(projectile, point.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().AddForce(bulletPath * projectileSpeed, ForceMode2D.Impulse);

        waveCounter++;

        if (waveCounter >= shootingPoints.Length)
        {
            waveCounter = 0;
        }
    }

    /// <summary>
    ///  For all the spawn points, spawn a projectile at the same time.
    /// </summary>
    public void burst()
    {
        shootTimeSpace = 1f;
        projectileSpeed = 0.5f;
        foreach (Transform point in shootingPoints)
        {
            Vector3 bulletPath = point.position;
            bulletPath.y = bulletPath.y * -1f;

            GameObject bullet = Instantiate(projectile, point.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().AddForce(bulletPath * projectileSpeed, ForceMode2D.Impulse);
        }
    }

    /// <summary>
    /// this shoots a big laser for a short period of time.
    /// </summary>
    public void laser()
    {
        shootTimeSpace = 0.05f;
        projectileSpeed = 3f;

        // Get the middle spawn point.
        Transform point = shootingPoints[2];

        Vector3 bulletPath = point.position;
        bulletPath.y = bulletPath.y * -1f;

        GameObject bullet = Instantiate(laserProjectile, point.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().AddForce(bulletPath * projectileSpeed, ForceMode2D.Impulse);

        waveCounter++;

        if (waveCounter >= shootingPoints.Length)
        {
            waveCounter = 0;
        }
    }
}
