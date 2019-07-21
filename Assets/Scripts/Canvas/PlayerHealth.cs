using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private Text playerHealthText;
    void Start()
    {
        this.playerHealthText = GetComponent<Text>();
    }

    void Update()
    {
        Player player = GameState.getInstance().getPlayer();
        int health = player.getHealth();
        int totalHealth = player.getTotalHealth();
        this.playerHealthText.text = health + " / " + totalHealth + " HP";
    }
}
