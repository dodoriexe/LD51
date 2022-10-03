using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Stomach : MonoBehaviour, IDropHandler
{

    public List<Color> mixedColors;
    Image image;
    Hand hand;

    private void Start()
    {
        image = GetComponent<Image>();
        mixedColors = new List<Color>();
        hand = FindObjectOfType<Hand>();
    }

    // Update is called once per frame
    void Update()
    {
        if(mixedColors.Count > 0)
        {
            image.color = CombineColors(mixedColors);
        }
    }

    public Color CombineColors(List<Color> colors)
    {
        Color result = new Color(0, 0, 0, 0);
        foreach (var item in colors)
        {
            result += item;
        }

        result /= colors.Count;
        return result;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if(eventData.pointerDrag != null)
        {
            Potion potion = eventData.pointerDrag.GetComponent<Potion>();
            potion.Drink();
            mixedColors.Add(potion.liquidColor);

            if(potion.stored)
            {
                Destroy(potion.gameObject);
                hand.storedPotion = null;
            }
            else
            {
                FindObjectOfType<PotionRoulette>().PotionDecided();
            }
        }
    }
}
