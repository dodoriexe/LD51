using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hand : MonoBehaviour, IDropHandler
{

    public Potion storedPotion;

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {

            if (storedPotion != null) return;

            Potion potion = eventData.pointerDrag.GetComponent<Potion>();

            potion.stored = true;
            storedPotion = potion;

            potion.transform.SetParent(this.transform);

            potion.rectTransform.anchoredPosition = transform.position;
            potion.canvasGroup.blocksRaycasts = true;
            potion.canvasGroup.alpha = 1f;

            FindObjectOfType<PotionRoulette>().PotionDecided();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
