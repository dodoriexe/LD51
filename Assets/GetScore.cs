using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetScore : MonoBehaviour
{
    Scoreboy scoreBoy;
    public TextMeshProUGUI thatText;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //thatText.text.Replace("X", scoreBoy.numOfPotions.ToString());
    }

    public void SetText(string waow)
    {
        thatText.text = waow;
    }
}
