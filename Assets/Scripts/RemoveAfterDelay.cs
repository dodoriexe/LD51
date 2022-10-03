using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveAfterDelay : MonoBehaviour
{
    public float delay;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RemoveDelay());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator RemoveDelay() {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
