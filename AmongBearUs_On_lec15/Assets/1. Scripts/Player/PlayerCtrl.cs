using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerCtrl : MonoBehaviour
{
    public GameObject JoyStick, mainView, playView;
    public Settings settings_scripts;
    public Button btn;
    public Sprite use, kill;
    public Text text_cool;
    public PlayerMoneyClass useClass;

    Animator anim;
    GameObject coll;

    KillCtrl killCtrl_Script;

    public float speed;
    public int money;

    public bool isCantMove, isMission;

    float timer;
    bool isCool,isAnim;

    private void Start()
    {
        anim = GetComponent<Animator>();

        Camera.main.transform.parent = transform;
        Camera.main.transform.localPosition = new Vector3(0, 0, -10);

        if (isMission)
        {
            btn.GetComponent<Image>().sprite = use;
            text_cool.text = "";
        }
        else
        {
            killCtrl_Script = FindObjectOfType<KillCtrl>();
            btn.GetComponent<Image>().sprite = kill;
            timer = 5;
            isCool = true;
        }
    }

    private void Update()
    {
        if (isCool)
        {
            timer -= Time.deltaTime;
            text_cool.text = Mathf.Ceil(timer).ToString();

            if(text_cool.text == "0")
            {
                text_cool.text = "";
                isCool = false;
            }
        }

        if( isCantMove)
        {
            JoyStick.SetActive(false);
        }
        else
        {
            Move();
        }

        if (isAnim && killCtrl_Script.kill_anim.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            killCtrl_Script.kill_anim.SetActive(false);
            killCtrl_Script.Kill();
            isCantMove = false;
            isAnim = false;

        }
        
    }

    // ĳ���� ������ ����
    void Move()
    {
        if (settings_scripts.isJoyStick)
        {
            JoyStick.SetActive(true);
        }
        else
        {
            JoyStick.SetActive(false);

            // ��Ŭ�� �Ǵ�
            if (Input.GetMouseButton(0))
            {
                if(!EventSystem.current.IsPointerOverGameObject())
                {
                    Vector3 dir = (Input.mousePosition - new Vector3(Screen.width * 0.5f, Screen.height * 0.5f)).normalized;

                    transform.position += dir * speed * Time.deltaTime;

                    anim.SetBool("isWalk", true);

                    // �������� �̵�
                    if (dir.x < 0)
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                    // ���������� �̵�
                    else
                    {
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                }               
            }
            // Ŭ������ ���� ��
            else
            {
                anim.SetBool("isWalk", false);
            }
        }
    }

    // ĳ���� ����
    public void DestroyPlayer()
    {
        Camera.main.transform.parent = null;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Mission" && isMission)
        {
            // print("�̼� ����");
            coll = collision.gameObject;

            btn.interactable = true;
        }

        if(collision.tag == "NPC" && !isMission && !isCool)
        {
            coll = collision.gameObject;
            btn.interactable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Mission")
        {
            coll = null;

            btn.interactable = false;
        }

        if (collision.tag == "NPC" && !isMission)
        {
            coll = null;
            btn.interactable = false;
        }
    }

    // USE��ư Ȱ��ȭ �� ������ ȣ��
    public void ClickButton()
    {
        if (isMission)
        {
            // MissionStart�� ȣ��
            coll.SendMessage("MissionStart");
        }
        else
        {
            Kill();
        }

        isCantMove = true;
        btn.interactable = false;
    }

    void Kill()
    {
        killCtrl_Script.kill_anim.SetActive(true);
        isAnim = true;

        coll.SendMessage("Dead");
        coll.GetComponent<CircleCollider2D>().enabled = false;
    }

    // �̼�â�� �����ϸ�(X ������) ȣ��
    public void MissionEnd()
    {
        isCantMove = false;
    }
}
