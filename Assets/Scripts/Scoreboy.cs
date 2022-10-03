using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scoreboy : MonoBehaviour
{
    public int numOfPotions;

    private void Start()
    {
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Menu") Destroy(this.gameObject);

        DontDestroyOnLoad(this);
        try
        {
            GameObject.Find("DeathText").GetComponent<GetScore>().SetText("Your body collapsed after only " + numOfPotions + " potions.");
        }
        catch (System.Exception)
        {

            //throw;
        }
    }
}
