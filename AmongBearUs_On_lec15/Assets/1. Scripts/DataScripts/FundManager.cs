using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FundManager : MonoSigletone<FundManager>
{
    [HideInInspector]
    public FUND_Base useBase;
    public List<Fund_Data> fundData;
    public GameObject fundSetObject;


    private void Start()
    {
        useBase = GetComponent<FUND_Base>();
    }

    public void FundStart()
    {
        StartCoroutine(useBase.FundValueRand());
        fundSetObject.gameObject.SetActive(true);
    }

    public void FundUpdate()
    {

        if (fundData.Count > GameManager.Inst.useClass.fundMoney.Count) GameManager.Inst.useClass.fundMoney.Add(0);

        for (int i = 0; i<fundData.Count; i++)
        {
            FundActive(fundData[i]);
            GameManager.Inst.useClass.fundMoney[i] = fundData[i].fundCell;
        }

    }

    public void FundActive(Fund_Data fund)
    {
        for(int i = 0; i<fund.useFund.Count; i++)
        {
            if(fund.useFund[i].num == useBase.FundValue)
            {
                ValueOnFund(fund, fund.useFund[i].fund);
            }
        }
    }



    public void ValueOnFund(Fund_Data fund, FundState useState)
    {
        fund.nowState = useState;
        StartCoroutine(UseValue(fund, useState));
    }

    IEnumerator UseValue(Fund_Data fund, FundState useState)
    {
        yield return new WaitForSeconds(5f);

        if (fund.fundCell == 0)
        {
            fund.fundCell += 50;
        }
        else
        {
            switch (fund.nowState)
            {
                case FundState.Normal:
                    Debug.Log("암것도 없다.");
                    break;
                case FundState.Good:
                    fund.fundCell += 50;
                    break;
                case FundState.Bad:
                    if (fund.fundCell > 50) fund.fundCell -= 50;
                    break;
                case FundState.Perfect:
                    fund.fundCell *= 2;
                    break;
                case FundState.Horrible:
                    if (fund.fundCell > 1000) fund.fundCell -= fund.fundCell / 3;
                    break;
            }
        }
    }
}
