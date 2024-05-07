using UnityEngine;

[CreateAssetMenu(menuName = "TM/StageTM", fileName = "StageTM_")]
public class StageTM : ScriptableObject {
    public int typeId;
    public int level;
    public int horizontalCount;
    public int VerticalCount;
    public int[] girdTypes;
}