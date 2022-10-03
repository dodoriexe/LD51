using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionRoulette : MonoBehaviour
{
    public List<GameObject> smallPotions;
    public List<GameObject> bigPotions;
    public List<GameObject> potionPool;

    public GameObject potionHolder1;
    public GameObject potionHolder2;
    public GameObject potionHolder3;

    public PotionPickerAnims potionPicker;
    public PotionSellerAnims potionSeller;

    public TMPro.TextMeshProUGUI potionName;
    public TMPro.TextMeshProUGUI potionDesc;

    public TMPro.TextMeshProUGUI countdownText;

    bool shopOpen = false;
    float currentTime = 0f;
    float startingTime = 10f;
    public int numOfIterations = 1;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
        potionName.text = "";
        potionDesc.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(!shopOpen)
        {
            currentTime -= 1 * Time.deltaTime;
            countdownText.text = currentTime.ToString("0.0");

            if (currentTime <= 0)
            {
                currentTime = 0;
                OpenShop();
            }
        }
        
    }

    public void ChangePotionInfo(string name, string desc)
    {
        potionName.text = name;
        potionDesc.text = desc;
    }

    void OpenShop()
    {
        GameObject potion1;
        GameObject potion2;
        GameObject potion3;

        shopOpen = true;

        if (numOfIterations % 6 == 0)
        {
            foreach (var item in bigPotions)
            {
                potionPool.Add(item);
            }

            //TODO 
        }
        else
        {
            if (numOfIterations % 2 == 0)
            {
                Level level = FindObjectOfType<Level>();
                level.potionIterations = numOfIterations;
                level.GenerateWave();

            }

            foreach (var item in smallPotions)
            {
                potionPool.Add(item);
            }
        }
        potionPicker.Show();
        potionSeller.Show();

        potion1 = potionPool[Random.Range(0, potionPool.Count)];
        potionPool.Remove(potion1);
        potion2 = potionPool[Random.Range(0, potionPool.Count)];
        potionPool.Remove(potion2);
        potion3 = potionPool[Random.Range(0, potionPool.Count)];
        potionPool.Remove(potion3);

        GameObject.Instantiate(potion1, potionHolder1.transform);
        GameObject.Instantiate(potion2, potionHolder2.transform);
        GameObject.Instantiate(potion3, potionHolder3.transform);
    }

    void CloseShop()
    {
        foreach (Transform item in potionHolder1.transform)
        {
            if (!item.GetComponent<Potion>().stored) Destroy(item.gameObject);
        }
        foreach (Transform item in potionHolder2.transform)
        {
            if (!item.GetComponent<Potion>().stored) Destroy(item.gameObject);
        }
        foreach (Transform item in potionHolder3.transform)
        {
            if (!item.GetComponent<Potion>().stored) Destroy(item.gameObject);
        }

        potionPool.Clear();

        potionName.text = "";
        potionDesc.text = "";

        currentTime = startingTime;
        potionPicker.Hide();
        potionSeller.Hide();
        shopOpen = false;

    }

    public void PotionDecided()
    {
        numOfIterations++;
        CloseShop();
    }

    IEnumerator EveryTenSeconds()
    {
        yield return new WaitForSeconds(10f);

    }

}
