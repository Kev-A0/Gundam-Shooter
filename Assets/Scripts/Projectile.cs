using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This is a projectile class for the laser
/// It makes the projectile disppear when it is 
/// out of the screen. 
/// 
/// Author: Brennen Chiu
/// Date: March 23, 2021; Revision: 1.0
/// </summary>
public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 out_pos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        float y_pos = GetComponent<Transform>().position.y;

        // Use the renderer and screen position to check if the Bullet has left the screen.
        if (!GetComponent<Renderer>().isVisible && y_pos >  out_pos.y)
        {
            // Delete the Enemy when it's off screen.
            Destroy(this.gameObject);

        }
    }
}
