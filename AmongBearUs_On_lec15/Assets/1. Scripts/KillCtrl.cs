using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCtrl : MonoBehaviour
{
    public Transform[] SpawnPoint;
    public GameObject kill_anim, text_anim,mainView;

    List<int> number = new List<int>();

    public GameObject nowText;

    int count;

    public void KillReset()
    {
        kill_anim.SetActive(false);
        text_anim.SetActive(false);
        number.Clear();
        for(int i = 0; i<SpawnPoint.Length; i++)
        {
            if(SpawnPoint[i].childCount != 0)
            {
                Destroy(SpawnPoint[i].GetChild(0).gameObject);
            }
        }
        NPCSpawn();
    }

    public void NPCSpawn()
    {
        int rand = Random.Range(0, 9);

        for(int i = 0; i<5;)
        {
            if (number.Contains(rand))
            {
                rand = Random.Range(0, 9);
            }
            else
            {
                number.Add(rand);
                i++;
            }
        }

        for(int i = 0; i<number.Count; i++)
        {
            Instantiate(Resources.Load("NPC"), SpawnPoint[number[i]]);
        }
    }

    public void Kill()
    {
        count++;
        if (count == 5)
        {
            text_anim.gameObject.SetActive(true);
            Invoke("Change", 3f);
        }
    }

    public void Change()
    {
        count = 0;
        mainView.SetActive(true);
        gameObject.SetActive(false);

        FindObjectOfType<PlayerCtrl>().DestroyPlayer();

    }
}
