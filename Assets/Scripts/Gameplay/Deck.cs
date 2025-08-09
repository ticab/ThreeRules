using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Deck : MonoBehaviour, IPointerClickHandler
{
    public HandManager handManager;
    private List<GameObject> deckOfCards = new List<GameObject>();
    private int deckSize = 52;
    private float cardStackOffset = 0.3f;

    public GameObject deckCardPrefab;

    void Start()
    {
        AddCardsToDeck();
    }

    private void OnEnable()
    {
        for(int i=0; i<6; i++)
            EventSystem.OnStartGame += DrawCard;
    }

    private void AddCardsToDeck()
    {
        for (int i = 0; i < deckSize; i++)
        {
            GameObject newCard = Instantiate(deckCardPrefab, transform);
            deckOfCards.Add(newCard);
        }
        ArrangeDeck();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        DrawCard();
    }

    private void DrawCard()
    {
        if (handManager != null)
        {
            // Draw top card
            if (deckOfCards.Count > 0)
            {
                if (handManager.AddCardHand())
                {
                    GameObject topCard = deckOfCards[deckOfCards.Count - 1];
                    deckOfCards.RemoveAt(deckOfCards.Count - 1);
                    Destroy(topCard);
                }
            }
        }
    }

    void ArrangeDeck()
    {
        for (int i = 0; i < deckOfCards.Count; i++)
        {
            GameObject card = deckOfCards[i];
            float rotationAngle = (-0.2f * (i - (52 - 1) / 2f));
            card.transform.localRotation = Quaternion.Euler(0f, 0f, rotationAngle);

            card.transform.localPosition = new Vector3(i * cardStackOffset, i * cardStackOffset, 0);
        }
    }
}
