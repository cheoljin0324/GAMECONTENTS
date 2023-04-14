using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class Settings : MonoBehaviour
{
    public bool isJoyStick;
    public Image touchBtn, JoyStickBtn;
    public Color blue;
    public PlayerCtrl playerCtrl_script;

    GameObject mainView, playView;

    private void Start()
    {
        mainView = playerCtrl_script.mainView;
        playView = playerCtrl_script.playView;
    }

    // Setting(���� ��ư) ������ ȣ��
    public void ClickSetting()
    {
        gameObject.SetActive(true);
        playerCtrl_script.isCantMove = true;

    }

    // �������� ���ư���
    public void ClickBack()
    {
        if(!Directory.Exists(Application.dataPath + "/DataFile")) Directory.CreateDirectory(Application.dataPath + "/DataFile");
        string jsonFile = JsonUtility.ToJson(GameManager.Inst.useClass,true);
        File.WriteAllText(Application.dataPath + "/DataFile/Data.txt",jsonFile);
        FundManager.Inst.fundSetObject.SetActive(false);
        SceneManager.LoadScene("Main");
    }

    // ��ġ�̵�
    public void ClickTouch()
    {
        isJoyStick = false;
        touchBtn.color = blue;
        JoyStickBtn.color = Color.white;
    }

    // ���̽�ƽ �̵�
    public void ClickJoyStick()
    {
        isJoyStick = true;
        touchBtn.color = Color.white; 
        JoyStickBtn.color = blue;
    }

    // ���� ������
    public void ClickQuit()
    {
        mainView.SetActive(true);
        playView.SetActive(false);

        // ĳ���� ����
        playerCtrl_script.DestroyPlayer();
    }

}
