using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible for making the background scroll.
/// Create the illusion of scrolling game.
/// 
/// Author: Brennen Chiu
/// Date: March 22, 2021; Revision: 1.0
/// </summary>
public class SpaceBackgroundScroll : MonoBehaviour
{
    // Scrolling speed
    public float speed = 4f;
    Vector3 StartPosition;

    // Start is called before the first frame update
    void Start()
    {
        // the start position is equal to current trandform value
        // of the background image
        // this is variabilities at the start of the game where the
        // backgound would be
        StartPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // want to move downwards
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        // loop back when it hits -19.365 
        // the outter layer of background 
        if (transform.position.y < -19.365f)
        {
            transform.position = StartPosition;
        }
        
    }
}
