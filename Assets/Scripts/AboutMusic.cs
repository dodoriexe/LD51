using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AboutMusic : MonoBehaviour
{
    public static AboutMusic instance;

    // Start is called before the first frame update
    void Start()
    {
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

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            Destroy(this.gameObject);
        }
    }
}
