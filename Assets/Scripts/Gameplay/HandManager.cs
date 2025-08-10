using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform handTransform;
    [SerializeField] private List<GameObject> cardsInHand = new List<GameObject>();
    
    private float fanSpread = -7.5f;
    private float cardSpacing = 200f;
    private float verticalSpacing = 70f;
    public bool AddCardToHand(CardType cardType)
    {
        if (cardPrefab == null || handTransform == null) return false;

        GameObject newCard = Instantiate(cardPrefab, handTransform.position, Quaternion.identity, handTransform);

        CardUI cardComponent = newCard.GetComponent<CardUI>();
        if (cardComponent != null)
        {
            bool boosterActive = Random.value < 0.2f;   // 20% chance it's a booster
            cardComponent.UpdateCardVisual(cardType, false, boosterActive);
        }

        CardDrag dragComponent = newCard.GetComponent<CardDrag>();
        if (dragComponent != null)
        {
            dragComponent.SetDraggable(true);
        }

        cardsInHand.Add(newCard);
        UpdateHandVisuals();
        return true;
    }

    public void RemoveCard(GameObject card)
    {
        cardsInHand.Remove(card);

        // add new cards if that was last one in Hand
        if(cardsInHand.Count == 0)
        {
            EventSystem.TriggerEmptyHand();
        }
    }
    private void UpdateHandVisuals()
    {
        int cardCount = cardsInHand.Count;

        if (cardCount == 1)
        {
            cardsInHand[0].transform.localRotation = Quaternion.identity;
            cardsInHand[0].transform.localPosition = Vector3.zero;
            return;
        }

        for (int i = 0; i < cardCount; i++)
        {
            float rotationAngle = (fanSpread * (i - (cardCount - 1) / 2f));
            cardsInHand[i].transform.localRotation = Quaternion.Euler(0f, 0f, rotationAngle);

            float horizontalOffset = (cardSpacing * (i - (cardCount - 1) / 2f));

            float normalizedPosition = (2f * i / (cardCount - 1) - 1f);
            float verticalOffset = verticalSpacing * (1 - normalizedPosition * normalizedPosition);
            cardsInHand[i].transform.localPosition = new Vector3(horizontalOffset, verticalOffset, 0f);
        }
    }
}
