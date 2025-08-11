using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private TMP_Text musicValue;

    [SerializeField] private Slider SFXSlider;
    [SerializeField] private TMP_Text SFXValue;

    [SerializeField] private Toggle showTutorial;

    private void Start()
    {
        if (!SaveSystem.IsTutorialCompleted())
        {
            // default: unchecked
            showTutorial.isOn = true;
        }
    }

    public void OnSliderMusicChanged()
    {
        if (MusicSystem.Instance != null)
        {
            MusicSystem.Instance.SetMusicVolume(musicSlider.value);
            musicValue.text = Mathf.FloorToInt(musicSlider.value*100).ToString();
        }
        else
        {
            Debug.LogWarning("MusicManager instance not found!");
        }
    }

    public void OnSliderSFXChanged()
    {
        if (MusicSystem.Instance != null)
        {
            MusicSystem.Instance.SetSFXVolume(SFXSlider.value);
            SFXValue.text = Mathf.FloorToInt(SFXSlider.value * 100).ToString();
        }
        else
        {
            Debug.LogWarning("MusicManager instance not found!");
        }
    }
    public void ResetTutorial()
    {
        SaveSystem.SaveTutorialCompleted(!showTutorial.isOn);
    }
}
