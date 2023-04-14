using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission6 : MonoBehaviour
{
    public bool[] isColor = new bool[4];
    public RectTransform[] rights;
    public LineRenderer[] lines;

    Animator anim;
    PlayerCtrl playerCtrl_script;

    bool isDrag;

    Vector2 click_pos;
    LineRenderer line;

    float leftY, rightY;

    Color leftC, rightC;

    MissionCtrl missionCtrl_Scripts;

    public void ClickLine(LineRenderer click)
    {
        click_pos = Input.mousePosition;
        line = click;

        leftY = click.transform.parent.GetComponent<RectTransform>().anchoredPosition.y;

        leftC = click.transform.parent.GetComponent<Image>().color;

        isDrag = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        missionCtrl_Scripts = FindObjectOfType<MissionCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDrag)
        {
            line.SetPosition(1, new Vector3(Input.mousePosition.x - click_pos.x, Input.mousePosition.y - click_pos.y, -10));
            if (Input.GetMouseButtonUp(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if(Physics.Raycast(ray,out hit))
                {
                    GameObject rightLine = hit.transform.gameObject;
                    rightY = rightLine.GetComponent<RectTransform>().anchoredPosition.y;
                    rightC = rightLine.GetComponent<Image>().color;
                    line.SetPosition(1, new Vector3(550, rightY - leftY, -10));
                    if (leftC == rightC)
                    {
                        switch (leftY)
                        {
                            case 225: isColor[0] = true; break;
                            case 75: isColor[1] = true; break;
                            case -75: isColor[2] = true; break;
                            case -225: isColor[3] = true; break;
                        }
                    }
                    else
                    {
                        switch (leftY)
                        {
                            case 225: isColor[0] = false; break;
                            case 75: isColor[1] = false; break;
                            case -75: isColor[2] = false; break;
                            case -225: isColor[3] = false; break;
                        }
                    }
                    if (isColor[0] && isColor[1] && isColor[2] && isColor[3])
                    {
                        Invoke("MissionSuccess", 0.2f);
                    }
                }
                else
                {
                    line.SetPosition(1, new Vector3(0, 0, -10));
                }
                isDrag = false;
            }
        }
    }

    public void MissionStart()
    {
        anim.SetBool("isUp", true);
        playerCtrl_script = FindObjectOfType<PlayerCtrl>();

        for(int i = 0; i<4; i++)
        {
            isColor[i] = false;
            lines[i].SetPosition(1, new Vector3(0, 0, -10));
        }

        for(int i = 0; i<rights.Length; i++)
        {
            Vector3 temp = rights[i].anchoredPosition;

            int rand = Random.Range(0,4);
            rights[i].anchoredPosition = rights[rand].anchoredPosition;
            rights[rand].anchoredPosition = temp;
        }
    }

    public void ClickCancle()
    {
        anim.SetBool("isUp", false);
        playerCtrl_script.MissionEnd();
    }

    public void ClickHandle()
    {
        isDrag = true;
    }

    public void MissionSuccess()
    {
        ClickCancle();
        missionCtrl_Scripts.MissionSuccess(GetComponent<Collider2D>());
    }


}
