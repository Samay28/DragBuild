using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public Transform orientation;
    float Xrot;
    float Yrot;
    public float sensitivity = 100f;

    void Start()
    {
        // Cursor.visible = false;
    }

    void Update()
    {
        if (!GameManager.instance.isInventoryOpen)
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivity;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivity;

            Yrot += mouseX;
            Xrot -= mouseY;
            Xrot = Mathf.Clamp(Xrot, -90f, 90f);
            transform.rotation = Quaternion.Euler(Xrot, Yrot, 0);
            orientation.rotation = Quaternion.Euler(0, Yrot, 0);
        }

       
    }
}
