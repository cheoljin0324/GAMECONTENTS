using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FUND_Base : MonoBehaviour
{
    [HideInInspector]
    public int FundValue;
    NewsActive newsActive;

    private void Start()
    {
        newsActive = GetComponent<NewsActive>();
    }

    public void TextNews(int rand)
    {
        int rand2 = Random.Range(0, 3);
        switch (FundManager.Inst.fundData[rand].nowState)
        {
            case FundState.Bad:
                newsActive.NewsTextActive(FundManager.Inst.fundData[rand].BadStr[rand2]);
                break;
            case FundState.Good:
                newsActive.NewsTextActive(FundManager.Inst.fundData[rand].GoodStr[rand2]);
                break;
            case FundState.Horrible:
                newsActive.NewsTextActive(FundManager.Inst.fundData[rand].HorribleStr[rand2]);
                break;
            case FundState.Perfect:
                newsActive.NewsTextActive(FundManager.Inst.fundData[rand].PerfectStr[rand2]);
                break;
            default:
                Debug.Log("암것도 없어");
                break;
        }
    }

    public IEnumerator FundValueRand()
    {
        while (true)
        {
            int rand = Random.Range(0, FundManager.Inst.fundData.Count);
            FundValue = Random.Range(1, 10);
            int setRand = Random.Range(0, 3);

            if (setRand == 2) TextNews(rand);
            yield return new WaitForSeconds(5f);
            GetComponent<FundManager>().FundUpdate();
        }
    }
}
