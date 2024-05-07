using UnityEngine;

public class GridEntity {

    public int index;
    public bool enable;
    public Vector2 worldPos;

    public Vector2Int viewPos;
    public Vector3Int coordinatePos;

    public void Ctor(int index, bool enable) {
        this.index = index;
        this.enable = enable;
    }

    public void SetHasBubble() {

    }
}