using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainMenu : MonoBehaviour
{
    public GameObject missionView,KillView;

    // 게임 종료 버튼 누르면 호출
    public void ClickQuit()
    {
        #if UNITY_EDITOR
        // 유니티 에디터 실행 멈춤
        UnityEditor.EditorApplication.isPlaying = false;
        #else
          Application.Quit();
        #endif
    }
    // 미션 버튼 누르면 미니 게임 시작
    public void ClickMission()
    {
        gameObject.SetActive(false);
        missionView.SetActive(true);

        GameObject player = Instantiate(Resources.Load("Character"), new Vector3(0, -4, 0), Quaternion.identity) as GameObject;
        player.GetComponent<PlayerCtrl>().mainView = gameObject;
        player.GetComponent<PlayerCtrl>().playView = missionView;
        player.GetComponent<PlayerCtrl>().isMission = true;

        missionView.SendMessage("MissionReset");
        if (File.Exists(Application.dataPath + "DataFile/Data.txt"))
        {
            string file = File.ReadAllText(Application.dataPath + "DataFile/Data.txt");

            for (int i = 0; i < FundManager.Inst.fundData.Count; i++)
            {
                GameManager.Inst.useClass.fundMoney.Add(0);
            }
            GameManager.Inst.useClass = JsonUtility.FromJson<PlayerMoneyClass>(file);
            for (int i = 0; i < FundManager.Inst.fundData.Count; i++)
            {
                FundManager.Inst.fundData[i].fundCell = GameManager.Inst.useClass.fundMoney[i];
            }

        }
    }

    // 미션 버튼 누르면 미니 게임 시작
    public void ClickKill()
    {
        gameObject.SetActive(false);
        KillView.SetActive(true);

        GameObject player = Instantiate(Resources.Load("Character"), new Vector3(0, -4, 0), Quaternion.identity) as GameObject;
        player.GetComponent<PlayerCtrl>().mainView = gameObject;
        player.GetComponent<PlayerCtrl>().playView = KillView;
        player.GetComponent<PlayerCtrl>().isMission = false;

        KillView.SendMessage("KillReset");
    }
}
