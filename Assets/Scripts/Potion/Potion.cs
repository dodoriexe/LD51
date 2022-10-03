using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Potion : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector]
    public Level level;
    public string potionName;
    [TextArea(4, 10)]
    public string potionDescription;
    public Color liquidColor;
    public PotionType potionType;
    public bool stored;


    Canvas canvas;
    public CanvasGroup canvasGroup;
    PotionRoulette potionRoulette;

    Vector2 originalPoint;

    Image image;
    public RectTransform rectTransform;

    public virtual void Drink()
    {
        Debug.Log("Player drank the " + potionType.ToString() + " potion: \"" + potionName + "\"!");
    }

    public void Hold()
    {
        Debug.Log("Player is holding the " + potionType.ToString() + " potion: \"" + potionName + "\" for later.");

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.8f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        //TODO: If over the stomach, drink.

        if(eventData.pointerDrag != null)
        {
            rectTransform.anchoredPosition = originalPoint;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");

    }

    private void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        potionRoulette = FindObjectOfType<PotionRoulette>();
        level = FindObjectOfType<Level>();
        image = GetComponent<Image>();
        rectTransform = gameObject.GetComponent<RectTransform>();
        image.color = liquidColor;
        originalPoint = rectTransform.anchoredPosition;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter");
        potionRoulette.ChangePotionInfo(potionName, potionDescription);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnPointerExit");
    }
}

public enum PotionType
{
    SMALL,
    BIG
}
