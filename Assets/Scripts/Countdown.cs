using UnityEngine;
using TMPro;
using System.Collections;

public class Countdown : MonoBehaviour
{
    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private CanvasGroup canvasGroup;

    private float numberDuration = 1f;
    private Vector3 startScale = new Vector3(0.5f, 0.5f, 0.5f);
    private Vector3 endScale = new Vector3(1.5f, 1.5f, 1.5f);
    private void OnEnable()
    {
        GameEvents.OnPlay += StartCountdown;
    }

    private void OnDisable()
    {
        GameEvents.OnPlay -= StartCountdown;
    }

    public void StartCountdown()
    {
        StartCoroutine(CountdownRoutine());
    }

    private IEnumerator CountdownRoutine()
    {
        countdownText.gameObject.SetActive(true);
        canvasGroup.alpha = 1f;

        for (int count = 3; count > 0; count--)
        {
            yield return StartCoroutine(AnimateNumber(count.ToString()));
        }

        yield return StartCoroutine(AnimateNumber("GO!"));

        countdownText.gameObject.SetActive(false);
        canvasGroup.alpha = 1f;


        GameEvents.TriggerGameStart();
    }

    private IEnumerator AnimateNumber(string text)
    {
        Debug.Log(text);
        countdownText.text = text;
        transform.localScale = startScale;
        canvasGroup.alpha = 1f;

        float elapsed = 0f;

        while (elapsed < numberDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / numberDuration;

            // Scale animation
            transform.localScale = Vector3.Lerp(startScale, endScale, t);

            // Fade out
            if (t > 0.7f)
                canvasGroup.alpha = Mathf.Lerp(1f, 0f, (t - 0.7f) / 0.3f);

            yield return null;
        }
    }
}
