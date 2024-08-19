using UnityEngine;

public class SellUIRotation : MonoBehaviour
{
    [SerializeField] Transform mainCamera;
    [SerializeField] Transform sellUI;
    float rotationSpeed = 10f;

    void Update()
    {
        Vector3 directionToCamera = mainCamera.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(directionToCamera);
        Vector3 rotation = Quaternion.Lerp(sellUI.rotation, lookRotation, Time.unscaledDeltaTime * rotationSpeed).eulerAngles;
        sellUI.rotation = Quaternion.Euler(rotation);
    }
}
