using System;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

public struct PlayScore
{
    public int Score;
    public string PlayerName;

    public PlayScore(string name, int score)
    {
        PlayerName = name;
        Score = score;
    }
}

[Serializable]
public class HighScores
{
    public List<PlayScore> scores = new List<PlayScore>();
}

public static class SaveSystem
{
    private static string filePath = Path.Combine(Application.persistentDataPath, "scores.dat");
    

    public static List<PlayScore> LoadScores()
    {
        if (!File.Exists(filePath))
            return new List<PlayScore>();

        try
        {
            string json = File.ReadAllText(filePath);
            HighScores data = JsonUtility.FromJson<HighScores>(json);
            return data.scores;
        }
        catch
        {
            Debug.LogWarning("Failed to load scores.");
            return new List<PlayScore>();
        }
    }

    public static void SaveScore(int score, string playerName)
    {
        List<PlayScore> scores = LoadScores();

        int index = scores.FindIndex(s => score > s.Score);

        if (index != -1)
        {
            scores.Insert(index, new PlayScore(playerName, score));

            // Keep only top 5
            if (scores.Count > 5)
                scores.RemoveAt(5);
        }

        HighScores highScores = new HighScores { scores = scores };

        string json = JsonUtility.ToJson(highScores);
        File.WriteAllText(filePath, json);
    }
}
