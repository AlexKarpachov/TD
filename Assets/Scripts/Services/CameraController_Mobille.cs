using UnityEngine;

public class CameraController_Mobille : MonoBehaviour
{
    [SerializeField] float cameraMoveSpeed;

    private float initialHeight;
    const float cameraHeightOffset = 0f;

    private Vector2 previousTouchPosition;  
    private bool isDragging = false;        

    private void Start()
    {
        initialHeight = transform.position.y;
    }

    private void LateUpdate()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);  

            if (touch.phase == TouchPhase.Began)
            {
                previousTouchPosition = touch.position;  
                isDragging = true;  
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                Vector2 touchDelta = touch.position - previousTouchPosition;  

                Vector3 localMoveDirection = new Vector3(-touchDelta.x, 0f, -touchDelta.y).normalized;

                localMoveDirection = transform.TransformDirection(localMoveDirection);
                localMoveDirection.y = cameraHeightOffset;

                transform.localPosition += cameraMoveSpeed * Time.unscaledDeltaTime * localMoveDirection;

                previousTouchPosition = touch.position;  
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isDragging = false;  
            }
        }

        Vector3 newPosition = transform.position;
        newPosition.y = initialHeight;
        transform.position = newPosition;
    }
}
