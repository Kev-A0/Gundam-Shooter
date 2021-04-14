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
    /// <summary>
    /// This variables holds the current time.
    /// </summary>
    public float time;

    /// <summary>
    /// The variables holds a TimeSpan object that will help format the time.
    /// </summary>
    private TimeSpan currentInterval;

    /// <summary>
    /// The holds a reference to the Text Canvas element.
    /// Used to visually display the current time.
    /// </summary>
    private Text timeUItext;


    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        time = 0f;
        timeUItext = GetComponent<Text>();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary
    void Update()
    {
        // Add the amount of seconds that have passed since the last frame to get a consistant time.
        time += Time.deltaTime;

        // Format the time
        currentInterval = TimeSpan.FromSeconds(time);

        // Set the UI element to the current time.
        timeUItext.text = currentInterval.ToString(@"mm\:ss");
    }
}
