using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundOnOff : MonoBehaviour
{
    [SerializeField] private AudioSource cameraAudioSource;
    [SerializeField] private Button toggleButton;
    [SerializeField] private Sprite soundOnIcon;
    [SerializeField] private Sprite soundOffIcon;

    private bool isSoundOn = true;

    void Start()
    {
        toggleButton.onClick.AddListener(ToggleSound);
        UpdateButtonIcon();
    }

    void ToggleSound()
    {
        isSoundOn = !isSoundOn;
        cameraAudioSource.mute = !isSoundOn;
        UpdateButtonIcon();
    }

    void UpdateButtonIcon()
    {
        if (isSoundOn)
        {
            toggleButton.image.sprite = soundOnIcon;
        }
        else
        {
            toggleButton.image.sprite = soundOffIcon;
        }
    }
}
