using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSettings : MonoBehaviour
{
    private Toggle toggle;

    public string prop;

    void Start()
    {
        if (prop == null || prop == "") {
            throw new System.Exception("Error: Mising prop");
        }
        this.toggle = GetComponent<Toggle>();
        this.toggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt(this.prop));
        this.toggle.onValueChanged.AddListener(delegate {OnToggleValueChanged(); });
    }

    void OnToggleValueChanged()
    {
        PlayerPrefs.SetInt(this.prop, Convert.ToInt32(this.toggle.isOn));
        PlayerPrefs.Save();
    }
}
