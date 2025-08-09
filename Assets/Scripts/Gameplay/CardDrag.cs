using UnityEngine;
using UnityEngine.EventSystems;

public class CardDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private bool isDraggable = false; 
    public bool IsDraggable => isDraggable;

    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private Transform originalParent;
    private Vector2 originalPosition;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void SetDraggable(bool value) => isDraggable = value;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isDraggable)
        {
            canvasGroup.blocksRaycasts = false;
            originalParent = transform.parent;
            originalPosition = rectTransform.localPosition;

            transform.SetParent(canvas.transform);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDraggable)
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDraggable) return;

        canvasGroup.blocksRaycasts = true;
        if (transform.parent == canvas.transform)
        {
            transform.SetParent(originalParent);
            rectTransform.localPosition = originalPosition;
        }
    }
}
