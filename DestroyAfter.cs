﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{

    public float time = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Die", time);   
    }

    // Update is called once per frame
    void Die()
    {
        Destroy(gameObject);
    }
}
