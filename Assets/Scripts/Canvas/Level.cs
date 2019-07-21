using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    private Text levelText;
    void Start()
    {
        this.levelText = GetComponent<Text>();
    }

    void Update()
    {
        int level = GameState.getInstance().getLevel();
        this.levelText.text = "Level " + level;
    }
}
