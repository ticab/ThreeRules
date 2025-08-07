using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    [SerializeField] private int maxCards = 4;
    [SerializeField] private HandManager handManager;
    [SerializeField] private TrainManager trainManager;
    [SerializeField] private Transform battleCardPosition;
    [SerializeField] private Transform popUpPosition;

    [SerializeField] private GameObject popupPrefab;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObj = eventData.pointerDrag;

        if (droppedObj == null) return;

        if (transform.childCount >= maxCards)
        {
            Debug.Log("Drop zone full!");
            return;
        }

        CardDisplay card = droppedObj.GetComponent<CardDisplay>();
        if (card == null || !card.IsDraggable) return;

        handManager.RemoveCard(droppedObj);

        droppedObj.transform.SetParent(transform);

        CheckTrain(droppedObj, card);
    }

    private void CheckTrain(GameObject cardObject, CardDisplay card)
    {
        GameObject trainCardObject = trainManager.GetFirstCard();
        if (trainCardObject == null) return;

        trainManager.RemoveCard(trainCardObject);

        CardDisplay trainCard = trainCardObject.GetComponent<CardDisplay>();

        if (trainCard == null) return;
        
        StartCoroutine(Wiggle(trainCardObject.transform, battleCardPosition.transform.position));
        StartCoroutine(Wiggle(cardObject.transform));
        StartCoroutine(ShowPopup(trainCard.getCardType(), card.getCardType()));

        StartCoroutine(ResetAfterDelay(1f, trainCardObject, cardObject));
    }

    private IEnumerator ShowPopup(CardType enemy, CardType user, float duration = 1f)
    {
        GameObject popup = Instantiate(popupPrefab, popUpPosition.position, Quaternion.identity, popUpPosition);

        PopUp.PopUpType result = BattleOutcome.DetermineOutcome(enemy, user);
        popup.GetComponent<PopUp>().Setup(result);

        yield return new WaitForSeconds(duration);

        Destroy(popup);
    }

    private IEnumerator ResetAfterDelay(float delay, GameObject card1, GameObject card2)
    {
        yield return new WaitForSeconds(delay);
        Destroy(card1);
        Destroy(card2);

        trainManager.AddCard();
    }

    private IEnumerator Wiggle(Transform target, Vector3? finalPosition = null, float duration = 0.5f, float angle = 15f)
    {
        Vector3 start = target.position;
        Vector3 end = finalPosition ?? start;
        bool shouldMove = finalPosition.HasValue && finalPosition.Value != start;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            if (shouldMove) target.position = Vector3.Lerp(start, end, t);

            float wiggle = Mathf.Sin(t * Mathf.PI * 5f) * angle;  // wiggle 5 times
            target.localRotation = Quaternion.Euler(0, 0, wiggle);

            elapsed += Time.deltaTime;
            yield return null;
        }

        if (shouldMove) target.position = end;
        target.localRotation = Quaternion.identity;
    }
}
