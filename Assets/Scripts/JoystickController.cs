using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public float sensitivity = 2f;
    private Vector3 inputDirection = Vector3.zero; 

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 input = eventData.position - new Vector2(transform.position.x, transform.position.y);
        inputDirection = input.normalized;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        inputDirection = Vector3.zero;
    }


    public Vector3 GetInputDirection()
    {
        return inputDirection;
    }
}
