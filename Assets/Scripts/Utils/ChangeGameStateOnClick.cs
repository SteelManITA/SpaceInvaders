using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeGameStateOnClick : MonoBehaviour
{
    private Button button;
    public GameState.State state;

    void Start()
    {
        this.button = GetComponent<Button>();
        this.button.onClick.AddListener(delegate {OnButtonClick(); });
    }

    void OnButtonClick()
    {
        switch (this.state) {
            case GameState.State.Started:
                GameState.getInstance().start();
                break;
            case GameState.State.Paused:
                GameState.getInstance().pause();
                break;
            case GameState.State.Stopped:
                GameState.getInstance().stop();
                break;
            default:
                throw new System.Exception("GameState.State: unrecognized State");
        }
    }
}
