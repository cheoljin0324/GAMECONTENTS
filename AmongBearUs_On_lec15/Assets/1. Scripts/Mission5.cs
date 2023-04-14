using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Mission5 : MonoBehaviour
{

    Vector2 oriPos;
    public Color blue, red;
    public Transform rotate, handle;

    Animator anim;
    PlayerCtrl playerCtrl_script;
    RectTransform rect_handle;

    MissionCtrl missionCtrl_Scripts;

    bool isDrag, isPlay;
    float rand;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rect_handle = handle.GetComponent<RectTransform>();

        rand = 0;

        rand = Random.Range(-195, 195);
        while(rand>=10 && rand <= 10)
        {
            rand = Random.Range(-195, 195);
        }
        rect_handle.anchoredPosition = new Vector2(184, rand);

        missionCtrl_Scripts = FindObjectOfType<MissionCtrl>();
    }

    private void Update()
    {
        if (isPlay)
        {
            // �巡��
            if (isDrag)
            {
                handle.position = Input.mousePosition;
                rect_handle.anchoredPosition = new Vector2(184,
                Mathf.Clamp(rect_handle.anchoredPosition.y, -195, 195));

                // �巡�� ��
                if (Input.GetMouseButtonUp(0))
                {
                    if(rect_handle.anchoredPosition.y > -5 && rect_handle.anchoredPosition.y < 5)
                    {
                        Invoke("MissionSuccess", 0.2f);
                        isPlay = true;
                    }
                    isDrag = false;
                }
            }
            rotate.eulerAngles = new Vector3(0, 0, 90 * rect_handle.anchoredPosition.y / 195);

            if (rect_handle.anchoredPosition.y > -5 && rect_handle.anchoredPosition.y < 5)
            {
                rotate.GetComponent<Image>().color = blue;
            }
            else
            {
                rotate.GetComponent<Image>().color = red;
            }
        }

    }

    //�̼� ����
    public void MissionStart()
    {
        anim.SetBool("isUp", true);
        playerCtrl_script = FindObjectOfType<PlayerCtrl>();
        isPlay = true;
    }

    // X ��ư ������ ȣ��
    public void ClickCancle()
    {
        anim.SetBool("isUp", false);
        playerCtrl_script.MissionEnd();
    }

    // ������ ������ ȣ��
    public void ClickHandle()
    {
        isDrag = true;
    }

    // �̼� �����ϸ� ȣ��
    public void MissionSuccess()
    {
        ClickCancle();
        missionCtrl_Scripts.MissionSuccess(GetComponent<Collider2D>());
    }


}
    
