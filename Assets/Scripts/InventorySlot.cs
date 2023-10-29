using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public GameObject prefab3D; 
    public Transform worldContainer; 

    private RectTransform itemTransform; 
    private CanvasGroup canvasGroup; 
    private Camera mainCamera; 
    private Vector3 initialScale; 
    private bool hasBeenReplaced = false; 

    private void Start()
    {
        itemTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        mainCamera = Camera.main;
        initialScale = itemTransform.localScale;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.5f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!hasBeenReplaced)
        {

            Ray ray = mainCamera.ScreenPointToRay(eventData.position);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                itemTransform.anchoredPosition += eventData.delta;
                itemTransform.localScale = initialScale;
                itemTransform.position = hit.point;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if (!RectTransformUtility.RectangleContainsScreenPoint(itemTransform, eventData.position) && !hasBeenReplaced)
        {
            GameObject worldObject = Instantiate(prefab3D, CalculateWorldPosition(), Quaternion.identity, worldContainer);
            hasBeenReplaced = true;

            itemTransform.anchoredPosition = new Vector2(-5000f, -5000f); 
            itemTransform.gameObject.SetActive(false);
        }
    }

    private Vector3 CalculateWorldPosition()
    {
        // Implement logic to calculate the position in the world where the 3D object should be placed.
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            return hit.point; 
        }
        return Vector3.zero;
    }
}
