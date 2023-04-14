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

    // Setting(설정 버튼) 누르면 호출
    public void ClickSetting()
    {
        gameObject.SetActive(true);
        playerCtrl_script.isCantMove = true;

    }

    // 게임으로 돌아가기
    public void ClickBack()
    {
        if(!Directory.Exists(Application.dataPath + "/DataFile")) Directory.CreateDirectory(Application.dataPath + "/DataFile");
        string jsonFile = JsonUtility.ToJson(GameManager.Inst.useClass,true);
        File.WriteAllText(Application.dataPath + "/DataFile/Data.txt",jsonFile);
        FundManager.Inst.fundSetObject.SetActive(false);
        SceneManager.LoadScene("Main");
    }

    // 터치이동
    public void ClickTouch()
    {
        isJoyStick = false;
        touchBtn.color = blue;
        JoyStickBtn.color = Color.white;
    }

    // 조이스틱 이동
    public void ClickJoyStick()
    {
        isJoyStick = true;
        touchBtn.color = Color.white; 
        JoyStickBtn.color = blue;
    }

    // 게임 나가기
    public void ClickQuit()
    {
        mainView.SetActive(true);
        playView.SetActive(false);

        // 캐릭터 삭제
        playerCtrl_script.DestroyPlayer();
    }

}
