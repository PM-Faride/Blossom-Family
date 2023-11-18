using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FirstTimeData
{
    public bool firstTime;
    public FirstTimeData(bool info)
    {
        firstTime = info;
        //return info;
    }
}
