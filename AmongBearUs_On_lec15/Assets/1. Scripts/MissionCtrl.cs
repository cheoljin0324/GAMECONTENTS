using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MissionCtrl : MonoBehaviour
{

    int missionCount;

    public CircleCollider2D[] cols;

    public Slider guage;

    public GameObject text_anim, mainView;

    public void MissionReset()
    {
        guage.value = 0;
        missionCount = 0;

        for(int i = 0; i<cols.Length; i++)
        {
            cols[i].enabled = true;
        }

        text_anim.SetActive(false);
    }

    public void Change()
    {
        mainView.SetActive(true);
        gameObject.SetActive(false);

        FindObjectOfType<PlayerCtrl>().DestroyPlayer();
        SceneManager.LoadScene("SampleScene");
    }

    public void MissionSuccess(Collider2D col)
    {
        missionCount++;

        guage.value = missionCount / 7f;
        col.enabled = false;

        if(guage.value == 1)
        {
            text_anim.SetActive(true);
            Invoke("Change", 3f);
        }

    }
}
