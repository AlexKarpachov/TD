using UnityEngine;
using UnityEngine.UI;

public class GameSpeed : MonoBehaviour
{
    [SerializeField] Sprite playIcon;
    [SerializeField] Sprite speedPlayIcon;
    [SerializeField] Button toggleButton;

    public static bool isSpeedOn;

    private void Start()
    {
        Time.timeScale = 1f;
        toggleButton.onClick.AddListener(SpeedToggle);
        toggleButton.image.sprite = speedPlayIcon;
        isSpeedOn = false;
    }
    public void SpeedToggle()
    {
        isSpeedOn = !isSpeedOn;

        UpdateButtonIcon();
        Time.timeScale = isSpeedOn ? 2f : 1f;
    }

    public void UpdateButtonIcon()
    {
        toggleButton.image.sprite = isSpeedOn ? playIcon : speedPlayIcon;
    }
}
