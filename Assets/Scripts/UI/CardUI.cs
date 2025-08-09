using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image cardColor;
    [SerializeField] private TMP_Text cardName;
    [SerializeField] private Image cardImage;

    [Header("Sprites")]
    [SerializeField] private Sprite rockSprite;
    [SerializeField] private Sprite paperSprite;
    [SerializeField] private Sprite scissorsSprite;

    private CardType cardType;
    public CardType CardType => cardType;


    private List<Color> colors = new List<Color>
    {
        new Color32(0, 151, 178, 255),
        new Color32(178, 27, 0, 255),
        new Color32(0, 102, 0, 255)
    };

    void Start()
    {
        UpdateCardVisual();
    }

    void UpdateCardVisual()
    {
        cardType = (CardType)Random.Range(0, System.Enum.GetValues(typeof(CardType)).Length);
        cardName.text = cardType.ToString();

        switch (cardType)
        {
            case CardType.Rock:
                cardImage.sprite = rockSprite;
                break;
            case CardType.Paper:
                cardImage.sprite = paperSprite;
                break;
            case CardType.Scissors:
                cardImage.sprite = scissorsSprite;
                break;
        }
    }

    public void ChangeColor(bool random)
    {
        if (cardColor != null)
        {
            if (!random)
            {
                cardColor.color = new Color(0, 0, 0, 1);
            }
            else
            {
                cardColor.color = colors[Random.Range(0, colors.Count)];
            }
        }
    }
}

public enum CardType
{
    Rock,
    Paper,
    Scissors
}
