﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveScore : MonoBehaviour
{
    private Button continueBtn;
    private GameState state;

    public Text error;
    public InputField username;

    void Start()
    {
        this.continueBtn = GetComponent<Button>();
        this.continueBtn.onClick.AddListener(delegate {OnContinueBtnClick(); });
        this.error.enabled = false;
        this.state = GameState.getInstance();
    }

    void OnContinueBtnClick()
    {
        if (username.text == "") {
            this.error.enabled = true;
        } else {
            Ranking.Write(
                new Ranking(
                    this.username.text,
                    this.state.getScore(),
                    this.state.getLevel(),
                    0
                )
            );

            SceneManager.LoadScene("MainMenu");
        }
    }
}
