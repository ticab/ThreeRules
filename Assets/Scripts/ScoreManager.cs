using TMPro;
using UnityEngine;

public static class GameData
{
    public static int FinalScore;
}

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreValue;
    private int score = 0;

    private void OnEnable()
    {
        GameEvents.OnScore += UpdateScore;
    }

    private void UpdateScore(int amount)
    {
        score = Mathf.Max(0, score + amount);
        scoreValue.text = score.ToString();

        GameData.FinalScore = score;
    }
}
