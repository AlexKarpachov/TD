using UnityEngine;
using UnityEngine.UI;

public class SoundOnOff : MonoBehaviour
{
    [SerializeField] AudioSource cameraAudioSource;
    [SerializeField] Button toggleButton;
    [SerializeField] Sprite soundOnIcon;
    [SerializeField] Sprite soundOffIcon;

    bool isSoundOn = true;

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
        toggleButton.image.sprite = isSoundOn ? soundOnIcon : soundOffIcon;
    }
}
