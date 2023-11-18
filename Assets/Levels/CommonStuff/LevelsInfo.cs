using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelsInfo
{
    public int[,] levelsInformation;
    
    public LevelsInfo(int[,] info)
    {
        levelsInformation = info;
    }
}
