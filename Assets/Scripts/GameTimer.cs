using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        time += Time.deltaTime;
        currentInterval = TimeSpan.FromSeconds(time);
        text.text = currentInterval.ToString(@"mm\:ss");
    }
}
