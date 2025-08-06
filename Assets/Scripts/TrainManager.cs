using System.Collections.Generic;
using UnityEngine;

public class TrainManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform trainTransform;
    private float cardSpacing = 180f;
    private List<GameObject> cardsInTrain = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < 3; i++)
            AddCardHand();

        UpdateHandVisuals();
    }

    private void AddCardHand()
    {
        GameObject newCard = Instantiate(cardPrefab, trainTransform.position, Quaternion.identity, trainTransform);
        CardDisplay cardComponent = newCard.GetComponent<CardDisplay>();
        if (cardComponent != null)
        {
            cardComponent.ChangeColor(false);
        }
        else
        {
            Debug.LogWarning("Card component not found on the instantiated prefab.");
        }
        cardsInTrain.Add(newCard);
    }
    private void UpdateHandVisuals()
    {
        int cardCount = cardsInTrain.Count;

        if (cardCount == 1)
        {
            cardsInTrain[0].transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            cardsInTrain[0].transform.localPosition = new Vector3(0f, 0f, 0f);
            return;
        }

        for (int i = 0; i < cardCount; i++)
        {
            float horizontalOffset = (cardSpacing * (i - (cardCount - 1) / 2f));
            cardsInTrain[i].transform.localPosition = new Vector3(horizontalOffset, 0f, 0f);
        }
    }
}
