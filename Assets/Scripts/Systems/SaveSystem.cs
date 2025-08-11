using System;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

[Serializable]
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
public class HighScoresData
{
    public List<PlayScore> scores = new List<PlayScore>();
    public bool tutorialCompleted;
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
            HighScoresData data = JsonUtility.FromJson<HighScoresData>(json);
            return data.scores;
        }
        catch
        {
            Debug.LogWarning("Failed to load scores.");
            return new List<PlayScore>();
        }
    }

    public static bool IsTutorialCompleted()
    {
        if (!File.Exists(filePath))
            return false;

        try
        {
            string json = File.ReadAllText(filePath);
            HighScoresData data = JsonUtility.FromJson<HighScoresData>(json);
            return data.tutorialCompleted;
        }
        catch
        {
            Debug.LogWarning("Failed to load save file");
            return false;
        }
    }

    public static void SaveTutorialCompleted(bool isComplited)
    {
        List<PlayScore> scores = LoadScores();

        HighScoresData highScores = new HighScoresData { scores = scores, tutorialCompleted = isComplited };

        string json = JsonUtility.ToJson(highScores);
        File.WriteAllText(filePath, json);
    }

    public static void SaveScore(int score, string playerName)
    {
        List<PlayScore> scores = LoadScores();

        if (scores.Count == 0)
        {
            scores.Add(new PlayScore(playerName, score));
        }
        else
        {
            int index = scores.FindIndex(s => score > s.Score);

            if (index != -1)
            {
                scores.Insert(index, new PlayScore(playerName, score));

                // Keep only top 5
                if (scores.Count > 5)
                    scores.RemoveAt(5);
            }
            else return;
        }

        HighScoresData highScores = new HighScoresData { scores = scores, tutorialCompleted = IsTutorialCompleted() };

        string json = JsonUtility.ToJson(highScores);
        File.WriteAllText(filePath, json);

        EventSystem.TriggerHighScoresUI();
    }

    public static bool ShouldWrite(int score)
    {
        List<PlayScore> scores = LoadScores();

        int index = scores.FindIndex(s => score > s.Score);

        Debug.Log("index: " + index);

        if (index != -1 || scores.Count == 0) return true;

        return false;
    }
}
