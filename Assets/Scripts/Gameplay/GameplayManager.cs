
using System.Collections;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private HandManager handManager;
    [SerializeField] private TrainManager trainManager;
    [SerializeField] private Transform battleCardPosition;

    [SerializeField] private Transform popUpPosition;
    [SerializeField] private GameObject popupPrefab;

    public void HandleCardDrop(GameObject droppedObj, Transform dropZone)
    {
        // check if card exists and it is draggable
        CardDrag dragComponent = droppedObj.GetComponent<CardDrag>();
        CardUI cardComponent = droppedObj.GetComponent<CardUI>();
        if (dragComponent == null || !dragComponent.IsDraggable || cardComponent == null) return;

        // remove card from hand and add it to drop zone
        handManager.RemoveCard(droppedObj);
        droppedObj.transform.SetParent(dropZone);
        

        // get first card from train
        GameObject trainCardObject = trainManager.GetFirstCard();
        if (trainCardObject == null) return;

        // remove it and get card component
        trainManager.RemoveCard(trainCardObject);
        CardUI trainCard = trainCardObject.GetComponent<CardUI>();
        if (trainCard == null) return;

        // battle
        StartCoroutine(HandleBattleSequence(trainCardObject, droppedObj, trainCard, cardComponent));
    }

    private IEnumerator HandleBattleSequence(GameObject trainCardObject, GameObject droppedObj, CardUI trainCard, CardUI cardComponent)
    {
        // animations for train card and dropped card
        yield return StartCoroutine(RunAnimations(trainCardObject.transform, droppedObj.transform, battleCardPosition.transform.position));

        // pop up the result
        yield return StartCoroutine(ShowPopUp(trainCard.CardType, cardComponent));

        ResetCards(trainCardObject, droppedObj);
    }

    private IEnumerator RunAnimations(Transform trainCardTransform, Transform droppedTransform, Vector3 targetPosition)
    {
        Coroutine wiggleTrain = StartCoroutine(Wiggle(trainCardTransform, targetPosition));
        Coroutine wiggleDropped = StartCoroutine(Wiggle(droppedTransform));

        yield return wiggleTrain;
        yield return wiggleDropped;
    }

    private IEnumerator ShowPopUp(CardType enemy, CardUI user, float duration = 0.7f)
    {
        GameObject popUp = Instantiate(popupPrefab, popUpPosition.position, Quaternion.identity, popUpPosition);

        PopUpUI.PopUpType result = BattleOutcome.DetermineOutcome(enemy, user);
        popUp.GetComponent<PopUpUI>().Setup(result);

        yield return new WaitForSeconds(duration);

        Destroy(popUp);
    }

    private void ResetCards( GameObject card1, GameObject card2)
    {
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