using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public GameObject prefab3D; // Reference to the 3D object prefab
    public Transform worldContainer; // Reference to the container in the world where 3D objects should be placed

    private RectTransform itemTransform; // RectTransform of the item being dragged
    private CanvasGroup canvasGroup; // CanvasGroup for controlling the item's visibility
    private Camera mainCamera; // Reference to the main camera
    private Vector3 initialScale; // Store the initial scale of the UI element
    private bool hasBeenReplaced = false; // Track whether the UI object has been replaced by a 3D object

    private void Start()
    {
        itemTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        mainCamera = Camera.main;
        initialScale = itemTransform.localScale;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Make the item semi-transparent while dragging
        canvasGroup.alpha = 0.5f;

        // Allow the item to be interacted with while dragging
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!hasBeenReplaced)
        {
            // Cast a ray from the camera to the world to find the precise position
            Ray ray = mainCamera.ScreenPointToRay(eventData.position);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                itemTransform.anchoredPosition += eventData.delta;

                // Reset the item's scale to the initial scale to prevent scaling issues
                itemTransform.localScale = initialScale;
                // Update the position based on the raycast hit point
                itemTransform.position = hit.point;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Restore the item's visibility and interactivity
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        // Check if the item is dragged out of its panel
        if (!RectTransformUtility.RectangleContainsScreenPoint(itemTransform, eventData.position) && !hasBeenReplaced)
        {
            // Instantiate the 3D object and place it in the world
            GameObject worldObject = Instantiate(prefab3D, CalculateWorldPosition(), Quaternion.identity, worldContainer);
            hasBeenReplaced = true;

            // Deactivate the UI item, or move it out of view, instead of destroying it
            itemTransform.anchoredPosition = new Vector2(-5000f, -5000f); // Move it out of the visible area
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
            return hit.point; // Position the 3D object where the ray hits a collider in the world.
        }

        // If no hit is detected, return a default position or handle it as needed.
        return Vector3.zero;
    }
}
