using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinInPlace : MonoBehaviour
{
    public float degreesToSpin = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(0, 0, degreesToSpin * Time.deltaTime * 100);
    }
}
