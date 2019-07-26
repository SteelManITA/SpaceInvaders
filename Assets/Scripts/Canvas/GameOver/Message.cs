using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    private Text message;
    void Start()
    {
        this.message = GetComponent<Text>();
    }

    void Update()
    {
        GameState state = GameState.getInstance();
        int score = state.getScore();
        float time = state.getTime();
        this.message.text = "You scored " + score + " points in " + Utils.TimeToString(time) + ".\n\nGo see the ranking to find out if you are in the top 20!";
    }
}
