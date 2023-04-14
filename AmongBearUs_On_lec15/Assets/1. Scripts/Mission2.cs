using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class Mission2 : MonoBehaviour
{
    public Transform trash,handle;
    public GameObject bottom;
    public Animator shakeAnim;
    MissionCtrl missionCtrl_Scripts;

    RectTransform rect_handler;
    Vector2 oriPos;

    bool isPlay = true;

    Animator anim;
    PlayerCtrl playerCtrl_script;

    public Color red;
    public Image[] imgs;

    bool isDrag = false;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rect_handler = handle.GetComponent<RectTransform>();
        oriPos = rect_handler.anchoredPosition;

        missionCtrl_Scripts = FindObjectOfType<MissionCtrl>();
        isPlay = true;
    }

    // 미션 시작
    public void MissionStart()
    {
            anim.SetBool("isUp", true);
            playerCtrl_script = FindObjectOfType<PlayerCtrl>();

            for (int i = 0; i < trash.childCount; i++)
            {
                Destroy(trash.GetChild(i).gameObject);
            }

            for (int i = 0; i < 10; i++)
            {
                //사과
                GameObject trash5 = Instantiate(Resources.Load("Trash/Trash5"), trash) as GameObject;
                trash5.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-180, 180), Random.Range(-180, 180));
                trash5.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, Random.Range(0, 180));

                //사과
                GameObject trash4 = Instantiate(Resources.Load("Trash/Trash4"), trash) as GameObject;
                trash4.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-180, 180), Random.Range(-180, 180));
                trash4.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, Random.Range(0, 180));
            }

            for (int i = 0; i < 3; i++)
            {
                //사과
                GameObject trash3 = Instantiate(Resources.Load("Trash/Trash3"), trash) as GameObject;
                trash3.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-180, 180), Random.Range(-180, 180));
                trash3.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, Random.Range(0, 180));

                //사과
                GameObject trash2 = Instantiate(Resources.Load("Trash/Trash2"), trash) as GameObject;
                trash2.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-180, 180), Random.Range(-180, 180));
                trash2.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, Random.Range(0, 180));

                //사과
                GameObject trash1 = Instantiate(Resources.Load("Trash/Trash1"), trash) as GameObject;
                trash1.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-180, 180), Random.Range(-180, 180));
                trash1.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, Random.Range(0, 180));
            }
       
    }

    private void Update()
    {
        if (isPlay)
        {
            if (isDrag)
            {
                handle.position = Input.mousePosition;
                rect_handler.anchoredPosition = new Vector2(oriPos.x, Mathf.Clamp(rect_handler.anchoredPosition.y, -135, -47));

                shakeAnim.enabled = true;
                shakeAnim.Play("BackGround");

                if (Input.GetMouseButtonUp(0))
                {
                    rect_handler.anchoredPosition = oriPos;
                    isDrag = false;
                    shakeAnim.enabled = false;
                }

                if (rect_handler.anchoredPosition.y <= -130)
                {
                    bottom.SetActive(false);
                }
                else
                {
                    bottom.SetActive(true);
                }

                for (int i = 0; i < trash.childCount; i++)
                {
                    if (trash.GetChild(i).GetComponent<RectTransform>().anchoredPosition.y <= -600)
                    {
                        Destroy(trash.GetChild(i).gameObject);
                    }
                }

                if (trash.childCount == 0)
                {
                    Invoke("Succes", 0.2f);
                    isPlay = false;

                    rect_handler.anchoredPosition = oriPos;
                    isDrag = false;
                    shakeAnim.enabled = false;
                }
            }

           
        }
        
    }

    public void ClickHandler()
    {
        isDrag = true;

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
        isPlay = false;
        missionCtrl_Scripts.MissionSuccess(GetComponent<Collider2D>());
    }

    // X버튼 누르면 호출
    public void ClickCancle()
    {
        anim.SetBool("isUp", false);
        playerCtrl_script.MissionEnd();
    }
}
