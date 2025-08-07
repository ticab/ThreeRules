using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    public Image popUpImage;
    public Sprite winSprite;
    public Sprite loseSprite;
    public Sprite drawSprite;

    public enum PopUpType
    {
        Win,
        Lose,
        Draw
    }

    void Start()
    {
        
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
