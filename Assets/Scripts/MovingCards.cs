using UnityEngine;
using UnityEngine.EventSystems;

public class MovingCards : MonoBehaviour, IPointerClickHandler
{
    private bool isClicked = false;
    private Vector3 originalPosition;
    private float moveUpAmount = 100.0f;

    void Start()
    {
        originalPosition = transform.localPosition;
    }
   

    public void OnPointerClick(PointerEventData eventData)
    {
        isClicked = !isClicked;

        if (isClicked)
        {
            transform.localPosition = originalPosition + Vector3.up * moveUpAmount;
        }
        else
        {
            transform.localPosition = originalPosition;
        }
    }
}
