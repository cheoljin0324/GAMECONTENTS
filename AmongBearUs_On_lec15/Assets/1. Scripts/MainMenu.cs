using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainMenu : MonoBehaviour
{
    public GameObject missionView,KillView;

    // ���� ���� ��ư ������ ȣ��
    public void ClickQuit()
    {
        #if UNITY_EDITOR
        // ����Ƽ ������ ���� ����
        UnityEditor.EditorApplication.isPlaying = false;
        #else
          Application.Quit();
        #endif
    }
    // �̼� ��ư ������ �̴� ���� ����
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

    // �̼� ��ư ������ �̴� ���� ����
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
