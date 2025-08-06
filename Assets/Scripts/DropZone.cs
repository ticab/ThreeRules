using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    [SerializeField] private int maxCards = 5;
    public HandManager handManager;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObj = eventData.pointerDrag;

        if (droppedObj == null) return;

        if (transform.childCount >= maxCards)
        {
            Debug.Log("Drop zone full!");
            return;
        }

        handManager.RemoveCard(droppedObj);

        CardDisplay card = droppedObj.GetComponent<CardDisplay>();
        if (card != null)
        {
            droppedObj.transform.SetParent(transform);
        }
    }
}
