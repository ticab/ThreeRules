using UnityEngine;
using UnityEngine.UI;

public class PopUpUI : MonoBehaviour
{
    [SerializeField] private Image popUpImage;
    [SerializeField] private Sprite winSprite;
    [SerializeField] private Sprite loseSprite;
    [SerializeField] private Sprite drawSprite;

    public enum PopUpType
    {
        Win,
        Lose,
        Draw
    }

    public void Setup(PopUpType type)
    {
        switch (type)
        {
            case PopUpType.Win:
                popUpImage.sprite = winSprite;
                break;
            case PopUpType.Lose:
                popUpImage.sprite = loseSprite;
                break;
            case PopUpType.Draw:
                popUpImage.sprite = drawSprite;
                break;
        }
    }


}
