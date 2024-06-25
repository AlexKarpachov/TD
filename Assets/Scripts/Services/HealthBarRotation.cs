using UnityEngine;

public class HealthBarRotation : MonoBehaviour
{
    Camera mainCamera;
    [SerializeField] Transform enemyHB;
    float rotationSpeed = 10f;

    private void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
    }
    void Update()
    {
        Vector3 directionToCamera = mainCamera.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(directionToCamera);
        Vector3 rotation = Quaternion.Lerp(enemyHB.rotation, lookRotation, Time.unscaledDeltaTime * rotationSpeed).eulerAngles;
        enemyHB.rotation = Quaternion.Euler(rotation);
    }
}
