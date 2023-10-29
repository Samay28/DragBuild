using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float sensitivity = 2f; // Adjust sensitivity as needed

    private Vector2 inputDirection;
    private float rotationX = 0f;
    private float rotationY = 0f;

    void Update()
    {
        if (!GameManager.instance.isInventoryOpen)
        {
            inputDirection = FindObjectOfType<JoystickController>().GetInputDirection();

            rotationY += inputDirection.x * sensitivity * Time.deltaTime; // Horizontal rotation
            rotationX -= inputDirection.y * sensitivity * Time.deltaTime; // Vertical rotation
            rotationX = Mathf.Clamp(rotationX, -90f, 90f);

            transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);

            player.rotation = Quaternion.Euler(0, rotationY, 0);
        }
    }
}
