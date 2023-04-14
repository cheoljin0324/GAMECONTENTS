using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FundState
{
    Normal,Good,Bad,Perfect,Horrible
}

[System.Serializable]
public class Fund_Data
{
    public string ID;
    public string FundName;
    public int fundCell;
    [TextArea(5,5)]
    public string presString;
    public List<FundNum> useFund;
    public List<string> GoodStr;
    public List<string> BadStr;
    public List<string> PerfectStr;
    public List<string> HorribleStr;
    public FundState nowState;
}

[System.Serializable]
public class FundNum
{
    public FundState fund;
    public int num;
}
