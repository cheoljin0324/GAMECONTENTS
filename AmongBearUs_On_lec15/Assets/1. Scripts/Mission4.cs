using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Mission4 : MonoBehaviour
{
    Animator anim;
    PlayerCtrl plCtrl;
    MissionCtrl missionCtrl_Scripts;

    int count;

    public Color blue;
    public Transform numbers;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        missionCtrl_Scripts = FindObjectOfType<MissionCtrl>();
    }

    public void MissionStart()
    {
        anim.SetBool("isUp", true);
        plCtrl = FindObjectOfType<PlayerCtrl>();

        for(int i = 0; i<numbers.childCount; i++)
        {
            numbers.GetChild(i).GetComponent<Image>().color = Color.white;
            numbers.GetChild(i).GetComponent<Button>().enabled = true;
        }

        for (int i = 0; i < 10; i++)
        {
            Sprite temp = numbers.GetChild(i).GetComponent<Image>().sprite;

            int rand = Random.Range(0, 10);
            numbers.GetChild(i).GetComponent<Image>().sprite
            = numbers.GetChild(rand).GetComponent<Image>().sprite;

            numbers.GetChild(rand).GetComponent<Image>().sprite = temp;
        }

        count = 1;
    }

    public void ClickCancle()
    {
        anim.SetBool("isUp", false);
        plCtrl.MissionEnd();
    }

    public void ClickNumber()
    {
        if (count.ToString() == EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite.name) 
        {
            EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = blue;
            EventSystem.current.currentSelectedGameObject.GetComponent<Button>().enabled = false;
            count++;
        }

        if (count == 11)
        {
            Invoke("MissionSucces", 0.2f);
        }
    }

    public void MissionSucces()
    {
        ClickCancle();
        missionCtrl_Scripts.MissionSuccess(GetComponent<Collider2D>());
    }
}
