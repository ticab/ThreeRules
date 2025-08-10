using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image cardColor;
    [SerializeField] private TMP_Text cardName;
    [SerializeField] private Image cardImage;
    [SerializeField] private Image boosterEffect;

    [Header("Sprites")]
    [SerializeField] private Sprite rockSprite;
    [SerializeField] private Sprite paperSprite;
    [SerializeField] private Sprite scissorsSprite;

    private CardType cardType;
    private bool isBooster = false;
    public CardType CardType => cardType;
    public bool IsBooster => isBooster;


    private List<Color> colors = new List<Color>
    {
        new Color32(179, 34, 46, 255),
        new Color32(184, 134, 11, 255),
        new Color32(44, 83, 107, 255)
    };

    public void UpdateCardVisual(CardType cardType, bool trainCard, bool boosterActive = false)
    {
        this.cardType = cardType;
        cardName.text = cardType.ToString();

        switch (cardType)
        {
            case CardType.Rock:
                cardImage.sprite = rockSprite;
                cardColor.color = trainCard ? Color.black : colors[0];
                break;
            case CardType.Paper:
                cardImage.sprite = paperSprite;
                cardColor.color = trainCard ? Color.black : colors[1];
                break;
            case CardType.Scissors:
                cardImage.sprite = scissorsSprite;
                cardColor.color = trainCard ? Color.black : colors[2];
                break;
        }

        isBooster = boosterActive;

        if (boosterEffect != null)
            boosterEffect.gameObject.SetActive(boosterActive);
    }
}

public enum CardType
{
    Rock,
    Paper,
    Scissors
}
