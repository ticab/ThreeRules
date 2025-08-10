using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;
    public Image timerImage;

    private float totalTime = 60f;
    private float timeRemaining;
    private bool timerStarted = false;

    private void OnEnable()
    {
        EventSystem.OnStartGame += StartTimer;
    }
    private void OnDisable()
    {
        EventSystem.OnStartGame -= StartTimer;
    }

    private void StartTimer()
    {
        timeRemaining = totalTime;
        timerStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!timerStarted) return;

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateDisplay();
        }
        else
        {
            timeRemaining = 0;
            timerStarted = false;

            SceneManager.LoadScene(SceneName.GameOver.ToString());
        }
    }

    private void UpdateDisplay()
    {
        timerImage.fillAmount = timeRemaining / totalTime;
        timerText.text = Mathf.Max(0,Mathf.FloorToInt(timeRemaining)).ToString();
    }
}
