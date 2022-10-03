using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Member
{

    protected override Vector3 Combine()
    {
        return Vector3.zero; // We dont actually need this, cause the position will come from somewhere else.
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.position = transform.position;
    }
}
