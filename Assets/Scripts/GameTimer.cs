using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class keeps track of the time spent in the game.
/// Author: Kevin Lee
/// Date: March 20, 2021; Revision: 1.0
/// </summary>
public class GameTimer : MonoBehaviour
{
    public float time;
    private TimeSpan currentInterval;
    private Text text;


    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // Add the amount of seconds that have passed since the last frame to get a consistant time.
        time += Time.deltaTime;
        // Format the time
        currentInterval = TimeSpan.FromSeconds(time);
        // Set the UI element to the current time.
        text.text = currentInterval.ToString(@"mm\:ss");
    }
}
