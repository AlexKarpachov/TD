using UnityEngine;

public class HealthBarRotation : MonoBehaviour
{
    Camera mainCamera;
    [SerializeField] Transform enemyHealthBar;
    float rotationSpeed = 10f;

    private void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
    }
    void Update()
    {
        Vector3 directionToCamera = mainCamera.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(directionToCamera);
        Vector3 rotation = Quaternion.Lerp(enemyHealthBar.rotation, lookRotation, Time.unscaledDeltaTime * rotationSpeed).eulerAngles;
        enemyHealthBar.rotation = Quaternion.Euler(rotation);
    }
}
