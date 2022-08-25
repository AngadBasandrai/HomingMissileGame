using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    void Update()
    {
        transform.localScale += Vector3.one * 30f * Time.deltaTime;
        GetComponent<SpriteRenderer>().material.color = Color.Lerp(GetComponent<SpriteRenderer>().sharedMaterial.color, new Color(GetComponent<SpriteRenderer>().sharedMaterial.color.r, GetComponent<SpriteRenderer>().sharedMaterial.color.g, GetComponent<SpriteRenderer>().sharedMaterial.color.b, 0), 0.33333f * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D coll0)
    {
        if (coll0.tag == "Finish")
        {
            Destroy(gameObject);
        }
    }
}
