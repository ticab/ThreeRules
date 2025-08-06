using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image cardBg;
    public Image cardColor;
    public TMP_Text cardName;
    public Image CardImage;

    public Sprite rockSprite;
    public Sprite paperSprite;
    public Sprite scissorsSprite;

    private Card cardData;

    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private Transform originalParent;
    private Vector2 originalPosition;

    private bool isDragable = false;

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

        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        // Try to find the parent canvas
        canvas = GetComponentInParent<Canvas>();
    }

    void UpdateCardVisual()
    {
        cardName.text = cardData.cardName;

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

    public void setIsDragabble(bool isDragabble)
    {
        this.isDragable = isDragabble;
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
        else
        {
            Debug.LogWarning("cardColor is null! Make sure it is assigned in the prefab.");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click: " + cardName.text);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isDragable)
        {
            transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            canvasGroup.blocksRaycasts = false;
            originalParent = transform.parent;
            originalPosition = rectTransform.localPosition;

            transform.SetParent(canvas.transform);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(isDragable)
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDragable) return;

        canvasGroup.blocksRaycasts = true;
        if (transform.parent == canvas.transform)
        {
            transform.SetParent(originalParent);
            rectTransform.localPosition = originalPosition;
        }
    }
}
