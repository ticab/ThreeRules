using UnityEngine;


[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card: ScriptableObject
{
    public CardType cardType;
    public string cardName => cardType.ToString();
    public Sprite cardSprite;
}
public enum CardType
{
    Rock,
    Paper,
    Scissors
}


