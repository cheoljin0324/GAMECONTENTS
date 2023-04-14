using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Mission1 : MonoBehaviour
{
    Animator anim;
    PlayerCtrl playerCtrl_script;
    MissionCtrl missionCtrl_scripts;

    public Color red;
    public Image[] imgs;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        missionCtrl_scripts = FindObjectOfType<MissionCtrl>();
    }

    // 미션 시작
    public void MissionStart()
    {
        anim.SetBool("isUp", true);
        playerCtrl_script = FindObjectOfType<PlayerCtrl>();

        for (int i = 0; i < imgs.Length; i++)
        {
            imgs[i].color = red;
        }

        for (int i = 0; i < 4; i++)
        {
            int rand = Random.Range(0, 7);
            imgs[rand].color = Color.white;
        }
    }

    public void ClickButton()
    {
        Image img = EventSystem.current.currentSelectedGameObject.GetComponent<Image>();

        if (img.color == Color.white) img.color = red;
        else img.color = Color.white;

        int count = 0;

        for(int i=0; i<imgs.Length; i++)
        {
            if (imgs[i].color == Color.white) count++;
        }

        if(count == imgs.Length)
        {
            Invoke("Succes", 0.2f);
        }
    }

    public void Succes()
    {
        ClickCancle();
        missionCtrl_scripts.MissionSuccess(GetComponent<Collider2D>());
    }

    // X버튼 누르면 호출
     public void ClickCancle()
    {
        anim.SetBool("isUp", false);
        playerCtrl_script.MissionEnd();
    }
}
