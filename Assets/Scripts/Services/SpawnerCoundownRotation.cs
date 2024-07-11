using UnityEngine;
using UnityEngine.UI;

public class SpawnerCoundownRotation : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] Transform spawnerCD;
    [SerializeField] WavesCountdown wavesCountdown;
    [SerializeField] Image countDownIMG;

    float rotationSpeed = 10f;

    void Update()
    {
        Rotation();
        UpdateCountdownUI();
    }
    void Rotation()
    {
        Vector3 directionToCamera = mainCamera.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(directionToCamera);
        Vector3 rotation = Quaternion.Lerp(spawnerCD.rotation, lookRotation, Time.unscaledDeltaTime * rotationSpeed).eulerAngles;
        spawnerCD.rotation = Quaternion.Euler(rotation);
    }
    void UpdateCountdownUI()
    {
        float time = wavesCountdown.RemainingCountdownTime();
        float initialCountdownTime = wavesCountdown.InitialCountdownTime();

        if (initialCountdownTime > 0)
        {
            float normalizedTime = time / initialCountdownTime;
            countDownIMG.fillAmount = normalizedTime;
        }
        else
        {
            countDownIMG.fillAmount = 0f;
        }
    }
}
