using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    private float initialHeight;

    private void Start()
    {
        initialHeight = transform.position.y;
    }

    private void LateUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // To get direction vector to be able to move in the local coordinates
        Vector3 localMoveDirection = transform.TransformDirection(new Vector3(horizontalInput, 0f, verticalInput)).normalized;

        // to apply initial camera high
        localMoveDirection.y = 0f;

        // to move the camera in the local coordinates
        transform.localPosition += moveSpeed * Time.unscaledDeltaTime * localMoveDirection;

        // to save initial camera high after movement
        Vector3 newPosition = transform.position;
        newPosition.y = initialHeight;
        transform.position = newPosition;
    }
}
