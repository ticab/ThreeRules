using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputName;
    [SerializeField] private TMP_Text score;
    [SerializeField] private Button backButton;
    [SerializeField] private Button saveButton;
    [SerializeField] private TMP_Text playerName;

    private void Start()
    {
        score.text = GameData.FinalScore.ToString();
        bool shouldWrite = SaveSystem.ShouldWrite(GameData.FinalScore);
        
        backButton.gameObject.SetActive(!shouldWrite);

        saveButton.gameObject.SetActive(shouldWrite);
        playerName.gameObject.SetActive(shouldWrite);
        inputName.gameObject.SetActive(shouldWrite);
    }
    public void SaveScore()
    {
        string playerInput = inputName.text.Trim();
        string nameToSave = string.IsNullOrWhiteSpace(playerInput) ? "Unknown" : playerInput;

        SaveSystem.SaveScore(GameData.FinalScore, nameToSave);
        SceneManager.LoadScene(SceneName.MainMenu.ToString());
    }
}
