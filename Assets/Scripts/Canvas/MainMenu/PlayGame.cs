using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    private Button btn;

    void Start()
    {
        this.btn = GetComponent<Button>();
        this.btn.onClick.AddListener(delegate {OnBtnClick(); });
    }

    void OnBtnClick()
    {
        if (Convert.ToBoolean(PlayerPrefs.GetInt("PlayedTutorial", 0))) {
            SceneManager.LoadScene("Game");
        } else {
            SceneManager.LoadScene("Tutorial");
        }
    }
}
