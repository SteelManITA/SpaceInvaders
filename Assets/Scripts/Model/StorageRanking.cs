using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StorageRanking
{
    public string playerName;
    public int score;
    public int level;
    public float time;

    public StorageRanking(
        string playerName,
        int score,
        int level,
        float time
    ) {
        this.playerName = playerName;
        this.score = score;
        this.level = level;
        this.time = time;
    }

    public static void Write(StorageRanking currentRanking)
    {
        StorageRanking[] rankings = StorageRanking.GetAll();

        if (rankings.Length < 20) {
            int len = rankings.Length;
            Array.Resize(ref rankings, len + 1);
            rankings[len] = currentRanking;
        } else {
            if (rankings[19].score < currentRanking.score) {
                rankings[19] = currentRanking;
            } else {
                return;
            }
        }

        string json = JsonHelper.ToJson(rankings, true);

        PlayerPrefs.SetString("Rankings", json);
        PlayerPrefs.Save();
    }

    public static StorageRanking[] GetAll()
    {
        string rankingsString = PlayerPrefs.GetString("Rankings", "");

        if (rankingsString == "") {
            return new StorageRanking[0];
        }

        StorageRanking[] rankings = JsonHelper.FromJson<StorageRanking>(rankingsString);

        Array.Sort<StorageRanking>(
            rankings,
            new Comparison<StorageRanking>(
                (r1, r2) => r2.score.CompareTo(r1.score)
            )
        );

        return rankings.Where(r => r != null).ToArray();
    }
}
