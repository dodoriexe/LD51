using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Image bar;
    float lastHealth;
    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        bar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(lastHealth != playerController.currentHP)
        {
            ChangeHealth(playerController.currentHP, playerController.maxHP);
        }
    }

    void ChangeHealth(float healthValue, float maxHP)
    {
        float amount = (healthValue / maxHP);
        bar.fillAmount = amount;
    }
}
