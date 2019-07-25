using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    private Canvas gameOver;

    // Start is called before the first frame update
    void Start()
    {
        this.gameOver = GetComponent<Canvas>();
        this.gameOver.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        this.gameOver.enabled = !GameState.getInstance().getPlayer().isAlive();
    }
}
