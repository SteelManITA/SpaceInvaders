using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetEnabled : MonoBehaviour
{
    private Button button;

    public Canvas obj;
    public bool value;

    void Start()
    {
        this.button = GetComponent<Button>();
        this.button.onClick.AddListener(delegate {OnButtonClick(); });
    }

    void OnButtonClick()
    {
        this.obj.enabled = this.value;
    }
}
