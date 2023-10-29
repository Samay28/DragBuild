using UnityEngine;
using UnityEngine.Playables;

public class CameraController : MonoBehaviour
{
    public Transform orientation;
    public float sensitivity = 2f; // Adjust sensitivity as needed

    private float rotationX = 0f;
    private float rotationY = 0f;

    private Vector2 touchStart;

    void Update()
    {
        if (!GameManager.instance.isInventoryOpen)
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    touchStart = touch.position;
                    Debug.Log("touched");
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    Vector2 delta = touch.position - touchStart;
                    rotationX -= delta.y * sensitivity;
                    rotationX = Mathf.Clamp(rotationX, -90f, 90f);
                    rotationY += delta.x * sensitivity;

                    // Rotate the camera based on the swiping
                    orientation.rotation = Quaternion.Euler(0, rotationY, 0);
                    transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                    Debug.Log("move");
                }
            }
        }
    }
}
