using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    // Start is called before the first frame update
    private Text credits;
    void Start()
    {
        this.credits = GetComponent<Text>();
        this.credits.text = PlayerPrefs.GetInt("Credits", 0) + " credits";
    }

    // Update is called once per frame
    void Update()
    {
        this.credits.text = PlayerPrefs.GetInt("Credits", 0) + " credits";
    }
}
