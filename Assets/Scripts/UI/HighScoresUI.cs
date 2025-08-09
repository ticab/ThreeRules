using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoresUI : MonoBehaviour
{
    [SerializeField] private List<TMP_Text> names;
    [SerializeField] private List<TMP_Text> scores;
    [SerializeField] private TMP_Text noScores;
    [SerializeField] private List<TMP_Text> nameScore;

    void Start()
    {
        UpdateUI();
    }
    private void OnEnable()
    {
        EventSystem.OnHighScoresChanged += UpdateUI;
    }
    private void OnDisable()
    {
        EventSystem.OnHighScoresChanged -= UpdateUI;
    }

    private void UpdateUI()
    {
        List<PlayScore> savedScores = SaveSystem.LoadScores();

        if (savedScores.Count != 0)
        {
            noScores.gameObject.SetActive(false);
            foreach (var item in nameScore)
            {
                item.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (var item in nameScore)
            {
                item.gameObject.SetActive(false);
            }
            return;
        }


        for (int i = 0; i < savedScores.Count; i++)
        {
            names[i].text = (i + 1).ToString() + ". " + savedScores[i].PlayerName;

            scores[i].text = savedScores[i].Score.ToString();
        }
    }
}
