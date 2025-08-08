using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform handTransform;
    private float fanSpread = -7.5f;
    private float cardSpacing = 200f;
    private float verticalSpacing = 70f;
    private List<GameObject> cardsInHand = new List<GameObject>();

    public bool AddCardHand()
    {
        if(cardsInHand.Count == 6)
            return false;

        GameObject newCard = Instantiate(cardPrefab, handTransform.position, Quaternion.identity, handTransform);
        CardDisplay cardComponent = newCard.GetComponent<CardDisplay>();
        if (cardComponent != null)
        {
            cardComponent.ChangeColor(true);
            cardComponent.setIsDraggable(true);
        }
        else
        {
            Debug.LogWarning("Card component not found on the instantiated prefab.");
        }

        cardsInHand.Add(newCard);
        UpdateHandVisuals();
        return true;
    }

    public void RemoveCard(GameObject card)
    {
        cardsInHand.Remove(card);
    }
    private void UpdateHandVisuals()
    {
        int cardCount = cardsInHand.Count;

        if (cardCount == 1)
        {
            cardsInHand[0].transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            cardsInHand[0].transform.localPosition = new Vector3(0f, 0f, 0f);
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
