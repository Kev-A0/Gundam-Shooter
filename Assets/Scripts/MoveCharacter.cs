using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This is the movement class for the player.
/// The player can move up down left right with 
/// with keyboard pressed. 
/// 
/// Author: Brennen Chiu
/// Date: March 23, 2021; Revision: 1.0
/// </summary>
public class MoveCharacter : MonoBehaviour
{
    // speed of the object
    public float speed = 10.5f;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey("w"))
        {
            pos.y += speed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            pos.y -= speed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            pos.x += speed * Time.deltaTime;
        }
        if (Input.GetKey("a")) 
        {
            pos.x -= speed * Time.deltaTime;
        }

        transform.position = pos;
    }
}
