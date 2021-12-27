using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
    [SerializeField] private AudioClip hoverClip;
    [SerializeField] private AudioClip clickedClip;
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnHover()
    {
        audioSource.clip = hoverClip;
        audioSource.Play();
    }

    public void OnClick()
    {
        audioSource.clip = clickedClip;
        audioSource.Play();
    }
}
