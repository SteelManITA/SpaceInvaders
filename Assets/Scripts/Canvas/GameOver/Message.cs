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
        int ranking = 0;
        int players = 0;
        this.message.text = "You scored " + score + " points.\n\nYou are the " + ranking + " in the ranking on " + players + " players";
    }
}
