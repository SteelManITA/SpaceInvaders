using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Ranking
{
    public string playerName;
    public int score;
    public int level;
    public float time;

    public Ranking(
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

    public static void Write(Ranking currentRanking)
    {
        Ranking[] rankings = Ranking.GetAll();

        if (rankings.Length < 20) {
            int len = rankings.Length;
            Array.Resize(ref rankings, len + 1);
            rankings[len] = currentRanking;
        } else {
            rankings[19] = currentRanking;
        }

        string json = JsonHelper.ToJson(rankings, true);

        PlayerPrefs.SetString("Rankings", json);
        PlayerPrefs.Save();
    }

    public static Ranking[] GetAll()
    {
        string rankingsString = PlayerPrefs.GetString("Rankings", "");

        if (rankingsString == "") {
            return new Ranking[0];
        }

        Ranking[] rankings = JsonHelper.FromJson<Ranking>(rankingsString);

        Array.Sort<Ranking>(
            rankings,
            new Comparison<Ranking>(
                (r1, r2) => r2.score.CompareTo(r1.score)
            )
        );

        return rankings.Where(r => r != null).ToArray();
    }
}
