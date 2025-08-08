using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gameover : MonoBehaviour
{
    [SerializeField] private TMP_InputField playerName;
    [SerializeField] private TMP_Text score;

    private void Start()
    {
        score.text = GameData.FinalScore.ToString();
    }

    public void SaveScore()
    {
        SaveSystem.SaveScore(Int32.Parse(score.text), playerName.text);
    }
}
