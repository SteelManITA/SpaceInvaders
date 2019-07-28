using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackToMainMenu : MonoBehaviour
{
    private Button button;

    void Start()
    {
        this.button = GetComponent<Button>();
        this.button.onClick.AddListener(delegate {OnButtonClick(); });
    }

    void OnButtonClick()
    {
        IPlayer player = GameState.getInstance().getPlayer();
        player.hurt(player.getTotalHealth());
    }
}
