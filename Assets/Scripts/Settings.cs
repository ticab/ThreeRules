using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private TMP_Text musicValue;

    [SerializeField] private Slider SFXSlider;
    [SerializeField] private TMP_Text SFXValue;

    public void OnSliderMusicChanged()
    {
        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.SetMusicVolume(musicSlider.value);
            musicValue.text = Mathf.FloorToInt(musicSlider.value*100).ToString();
        }
        else
        {
            Debug.LogWarning("MusicManager instance not found!");
        }
    }

    public void OnSliderSFXChanged()
    {
        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.SetSFXVolume(SFXSlider.value);
            SFXValue.text = Mathf.FloorToInt(SFXSlider.value * 100).ToString();
        }
        else
        {
            Debug.LogWarning("MusicManager instance not found!");
        }
    }
}
