using UnityEngine;
using System.Collections.Generic;

public class StageEntity {

    public int typeId;
    public int level;
    public int horizontalCount;
    public int VerticalCount;
    // public int[] gridTypess;
    public List<int> gridTypes;

    public int currentFirstIndex;
    public int targetCore;

    public StageEntity() {
        gridTypes = new List<int>();
    }
    public void Ctor() {
        currentFirstIndex = gridTypes.Count - GridConst.ScreenGridCount;
    }
}