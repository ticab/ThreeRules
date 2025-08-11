using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Deck : MonoBehaviour
{
    public HandManager handManager;
    private List<GameObject> deckOfCards = new List<GameObject>();
    private List<CardType> cardTypes = new List<CardType>();

    private static System.Random rng = new System.Random();

    private int deckSize = 60;
    private float cardStackOffset = 0.3f;
    private int maxCards = 6;
    private float dealTime = 0.2f;

    public GameObject deckCardPrefab;

    void Start()
    {
        AddCardsToDeck();
    }

    private void OnEnable()
    {
        EventSystem.OnStartTutorial += DealHand;
        EventSystem.OnHandEmptied += DealHand;
    }
    private void OnDisable()
    {
        EventSystem.OnStartTutorial -= DealHand;
        EventSystem.OnHandEmptied -= DealHand;
    }

    private void AddCardsToDeck()
    {
        for (int i = 0; i < deckSize/3; i++) cardTypes.Add(CardType.Rock);
        for (int i = 0; i < deckSize/3; i++) cardTypes.Add(CardType.Paper);
        for (int i = 0; i < deckSize/3; i++) cardTypes.Add(CardType.Scissors);

        Shuffle(cardTypes);

        for (int i = 0; i < deckSize; i++)
        {
            GameObject newCard = Instantiate(deckCardPrefab, transform);
            deckOfCards.Add(newCard);
        }
        ArrangeDeck();
    }

    private void Shuffle<T>(List<T> list)
    {
        for(int i = list.Count - 1; i > 0; i--)
        {
            int rnd = rng.Next(0, i+1);
            (list[i], list[rnd]) = (list[rnd], list[i]);
        }
    }

    private void DealHand()
    {
        StartCoroutine(DealHandCoroutine());
    }

    private IEnumerator DealHandCoroutine()
    {
        for (int i = 0; i < maxCards; i++)
        {
            DrawCard();
            MusicSystem.Instance.PlayCardSFX();
            yield return new WaitForSeconds(dealTime);
        }
    }

    private void DrawCard()
    {
        if (handManager != null)
        {
            // Draw top card
            if (deckOfCards.Count > 0)
            {
                int lastIndex = deckOfCards.Count - 1;
                if (handManager.AddCardToHand(cardTypes[lastIndex]))
                {
                    GameObject topCard = deckOfCards[lastIndex];
                    deckOfCards.RemoveAt(lastIndex);
                    Destroy(topCard);
                }
            }
            else
            {
                SceneManager.LoadScene(SceneName.GameOver.ToString());
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
