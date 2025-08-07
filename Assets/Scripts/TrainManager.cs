using System.Collections.Generic;
using UnityEngine;

public class TrainManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform trainTransform;
    private float cardSpacing = 150f;
    private List<GameObject> cardsInTrain = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < 3; i++)
            AddCard();
        UpdateHandVisuals();
    }

    public void AddCard()
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

        UpdateHandVisuals();
    }
    private void UpdateHandVisuals()
    {
        int cardCount = cardsInTrain.Count;

        Debug.Log("hor");
        for (int i = cardCount - 1; i >=0; i--)
        {
            float horizontalOffset = (cardSpacing * (i - (cardCount - 1) / 2f ) );
            Debug.Log(i.ToString() + " -> " + horizontalOffset);
            cardsInTrain[i].transform.localPosition = new Vector3(horizontalOffset, 0f, 0f); 
            cardsInTrain[i].transform.SetSiblingIndex(cardCount - 1 - i);
        }
    }

    public GameObject GetFirstCard()
    {
        if(cardsInTrain.Count > 0)
            return cardsInTrain[0];
        
        return null;
    }

    public void RemoveCard(GameObject card)
    {
        if (cardsInTrain.Count > 0)
        {
            cardsInTrain.Remove(card);
            UpdateHandVisuals();
        }
    }
}
