using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "TM/StageTM", fileName = "StageTM_")]
public class StageTM : ScriptableObject {
    public int typeId;
    public int level;
    public int horizontalCount;
    public int VerticalCount;
    public int targetCore;

    // public int[] girdTypess;
    public List<int> gridTypes;

}