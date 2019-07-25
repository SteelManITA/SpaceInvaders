using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasInitStatus : MonoBehaviour
{
    public bool status = false;

    void Start()
    {
        GetComponent<Canvas>().enabled = this.status;
    }
}
