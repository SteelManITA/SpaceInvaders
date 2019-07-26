using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
    private Button btn;

    public string scene = "MainMenu";

    void Start()
    {
        this.btn = GetComponent<Button>();
        this.btn.onClick.AddListener(delegate {OnBtnClick(); });
    }

    void OnBtnClick()
    {
        SceneManager.LoadScene(this.scene);
    }
}
