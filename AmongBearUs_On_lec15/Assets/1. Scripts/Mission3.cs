using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Mission3 : MonoBehaviour
{
    Animator anim;
    PlayerCtrl playerCtrl_script;
    public Text InputText, keycode;
    MissionCtrl missionCtrl_Scripts;

    public Color red;
    public Image[] imgs;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        missionCtrl_Scripts = FindObjectOfType<MissionCtrl>();
    }

    public void ClickNumber()
    {
        if (InputText.text.Length <= 4)
        {
            InputText.text += EventSystem.current.currentSelectedGameObject.name;
        }

    }

    public void clickDel()
    {
        if(InputText.text != "")
        {
            InputText.text = InputText.text.Substring(0, InputText.text.Length - 1);
        }
    }

    // 미션 시작
    public void MissionStart()
    {
        anim.SetBool("isUp", true);
        playerCtrl_script = FindObjectOfType<PlayerCtrl>();

        InputText.text = "";
        keycode.text = "";

        for(int i = 0; i< 5; i++)
        {
            keycode.text += Random.Range(0, 10);
        }
    }

    public void ClickCheck()
    {
        if (InputText.text == keycode.text)
        {
            Invoke("Succes", 0.2f);
        }
    }

    public void ClickButton()
    {
        Image img = EventSystem.current.currentSelectedGameObject.GetComponent<Image>();

        if (img.color == Color.white) img.color = red;
        else img.color = Color.white;

        int count = 0;

        for (int i = 0; i < imgs.Length; i++)
        {
            if (imgs[i].color == Color.white) count++;
        }

        if (count == imgs.Length)
        {
            Invoke("Succes", 0.2f);
        }
    }

    public void Succes()
    {
        ClickCancle();
        missionCtrl_Scripts.MissionSuccess(GetComponent<Collider2D>());
    }

    // X버튼 누르면 호출
    public void ClickCancle()
    {
        anim.SetBool("isUp", false);
        playerCtrl_script.MissionEnd();
    }
}
