using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{
    private int VIEWS_COUNT = 3;
    private int currentScreen = 0;

    public Button prev;
    public Button next;

    public RectTransform[] canvasScreens = new RectTransform[3];
    public GameObject[] gameScreens = new GameObject[3];

    void Awake()
    {
        this.VIEWS_COUNT = Mathf.Min(this.canvasScreens.Length, this.gameScreens.Length);
    }

    private void UpdateView()
    {
        for (int i = 0; i < VIEWS_COUNT; ++i) {
            this.canvasScreens[i].GetComponent<CanvasGroup>().alpha = i == this.currentScreen ? 1f : 0f;
            this.canvasScreens[i].GetComponent<CanvasGroup>().blocksRaycasts = i == this.currentScreen;

            this.gameScreens[i].SetActive(i == this.currentScreen);
        }

        this.next.GetComponentInChildren<Text>().text = this.currentScreen != VIEWS_COUNT - 1 ? "Next" : "Play";
        this.prev.enabled = this.currentScreen != 0;

    }

    private void OnNextClick()
    {
        if (this.currentScreen == VIEWS_COUNT - 1) {
            SceneManager.LoadScene("Game");
            PlayerPrefs.SetInt("PlayedTutorial", 1);
            PlayerPrefs.Save();
        } else {
            ++this.currentScreen;
        }
        this.UpdateView();
    }

    private void OnPrevClick()
    {
        --this.currentScreen;
        this.UpdateView();
    }

    void Start()
    {
        this.next.onClick.AddListener(delegate {OnNextClick(); });
        this.prev.onClick.AddListener(delegate {OnPrevClick(); });

        this.UpdateView();
    }

    void Update()
    {
    }
}
