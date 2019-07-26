using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static string TimeToString(float time)
    {
        string minutes = "" + Mathf.Floor(time / 60);
        string seconds = "" + Mathf.RoundToInt(time % 60);

        if (minutes.Length == 1) {
            minutes = "0" + minutes;
        }
        if (seconds.Length == 1) {
            seconds = "0" + seconds;
        }

        return minutes + ":" + seconds;
    }
}
