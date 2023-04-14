using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCtrl : MonoBehaviour
{
    public Sprite[] idles, deads;
    SpriteRenderer sr;
    int rand;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        rand = Random.Range(0, 5);
        sr.sprite = idles[rand];


    }   

    public void Dead()
    {
        sr.sprite = deads[rand];
        sr.sortingOrder = -1;
    }
}
