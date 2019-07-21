using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text scoreText;
    void Start()
    {
        this.scoreText = GetComponent<Text>();
    }

    void Update()
    {
        int score = GameState.getInstance().getScore();
        this.scoreText.text = "Score: " + score;
    }
}
