using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Continue : MonoBehaviour
{
    private Button continueBtn;

    void Start()
    {
        this.continueBtn = GetComponent<Button>();
        this.continueBtn.onClick.AddListener(delegate {OnContinueBtnClick(); });
    }

    void OnContinueBtnClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
