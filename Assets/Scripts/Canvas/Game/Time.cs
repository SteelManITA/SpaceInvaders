using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Time : MonoBehaviour
{
    private Text timeText;
    void Start()
    {
        this.timeText = GetComponent<Text>();
    }

    void Update()
    {
        int time = (int) GameState.getInstance().getTime();
        this.timeText.text = "Time: " + time;
    }
}
