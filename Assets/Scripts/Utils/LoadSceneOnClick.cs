using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
    private Button continueBtn;

    public string scene = "MainMenu";

    void Start()
    {
        this.continueBtn = GetComponent<Button>();
        this.continueBtn.onClick.AddListener(delegate {OnContinueBtnClick(); });
    }

    void OnContinueBtnClick()
    {
        SceneManager.LoadScene(this.scene);
    }
}
