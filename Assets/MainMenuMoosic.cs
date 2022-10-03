using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuMoosic : MonoBehaviour
{
    public static MainMenuMoosic instance;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "SampleScene")
        {
            Destroy(this.gameObject);
        }

        if (SceneManager.GetActiveScene().name == "About")
        {
            Destroy(this.gameObject);
        }
    }
}
