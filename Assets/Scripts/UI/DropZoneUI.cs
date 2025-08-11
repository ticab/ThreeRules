using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZoneUI : MonoBehaviour, IDropHandler
{
    [SerializeField] private int maxCards = 3;

    [SerializeField] private GameplayManager gameplayManager;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObj = eventData.pointerDrag;

        if (droppedObj == null) return;

        if (transform.childCount >= maxCards)
        {
            Debug.Log("Drop zone full!");
            return;
        }

        gameplayManager.HandleCardDrop(droppedObj, this.transform);
    }
}
