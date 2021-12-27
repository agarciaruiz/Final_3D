using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] Profiles m_profile;
    [SerializeField] List<Sliders> volumeSliders = new List<Sliders>();

    public Profiles profile
    {
        get { return m_profile; }
        set { Settings.profile = m_profile; }
    }

    private void Awake()
    {
        if(m_profile != null) 
            m_profile.SetProfile(m_profile);
    }

    private void Start()
    {
        if (Settings.profile && Settings.profile.audioMixer != null)
        {
            Settings.profile.GetAudioLevels();
        }
    }

    public void ApplyChanges()
    {
        if (Settings.profile && Settings.profile.audioMixer != null)
        {
            Settings.profile.SaveAudioLevels();
        }
    }

    public void CancelChanges()
    {
        if (Settings.profile)
        {
            Settings.profile.GetAudioLevels();
        }

        for(int i = 0; i < volumeSliders.Count; i++)
        {
            volumeSliders[i].ResetSliderValue();
        }
    }
}
