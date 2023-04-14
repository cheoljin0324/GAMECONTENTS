using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewsActive : MonoBehaviour
{
    public Text newsText;

    private void Start()
    {
        newsText.text = "";
    }

    public void NewsTextActive(string useExnews)
    {
        StartCoroutine(newsTextUpdate(useExnews));
    }

    IEnumerator newsTextUpdate(string news)
    { 
        newsText.text = news;
        yield return new WaitForSeconds(3f);
        newsText.text = "";
    }

    
}
