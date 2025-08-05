using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Image cardBg;
    public Image cardColor;
    public TMP_Text cardName;
    public Image CardImage;

    public Sprite rockSprite;
    public Sprite paperSprite;
    public Sprite scissorsSprite;

    private Card cardData;

    private List<Color> colors = new List<Color>
    {
        new Color32(0, 151, 178, 255),
        new Color32(178, 27, 0, 255),
        new Color32(0, 102, 0, 255)
    };

    void Start()
    {
        cardData = ScriptableObject.CreateInstance<Card>();
        cardData.cardType = (CardType) Random.Range(0, System.Enum.GetValues(typeof(CardType)).Length);
        UpdateCardVisual();
    }

    void UpdateCardVisual()
    {
        cardName.text = cardData.cardName;

        if (cardColor != null)
        {
            cardColor.color = colors[Random.Range(0, colors.Count)];
        }

        switch (cardData.cardType)
        {
            case CardType.Rock:
                CardImage.sprite = rockSprite;
                break;
            case CardType.Paper:
                CardImage.sprite = paperSprite;
                break;
            case CardType.Scissors:
                CardImage.sprite = scissorsSprite;
                break;
        }
    }
}
