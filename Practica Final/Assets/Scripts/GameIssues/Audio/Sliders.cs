using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

[RequireComponent(typeof (Slider))]
public class Sliders : MonoBehaviour
{
    Slider slider
    {
        get { return GetComponent<Slider>(); }
    }

    public AudioMixer audioMixer;
    [SerializeField] private string volumeName;

    private void Start()
    {
        ResetSliderValue();
        UpdateValueOnChange(slider.value);

        slider.onValueChanged.AddListener(delegate
        {
            UpdateValueOnChange(slider.value);
        });
    }

    public void UpdateValueOnChange(float value)
    {
        if (Settings.profile)
        {
            Settings.profile.SetAudioLevels(volumeName, value);
        }
    }

    public void ResetSliderValue()
    {
        if (Settings.profile)
        {
            float volume = Settings.profile.GetAudioLevels(volumeName);
            UpdateValueOnChange(volume);
            slider.value = volume;
        }
    }
}
