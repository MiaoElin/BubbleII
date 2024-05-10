using UnityEngine;

public class GridEntity {

    public int index;
    public int typeId;
    public bool enable;
    public bool isinSingular;
    public bool hasBubble;
    public int bubbleId;
    public ColorType colorType;
    public Vector2 worldPos;
    public Vector2Int viewPos;
    // public Vector3Int coordinatePos;

    public bool hasSearchColor;

    public bool isNeedFalling;
    public bool hasSearchTraction;

    public void Ctor(int index, Vector2Int viewPos) {
        this.index = index;
        hasSearchColor = false;
        this.viewPos = viewPos;
        isNeedFalling = false;
    }

    public void Reuse() {
        hasSearchColor = false;
        hasSearchTraction = false;
        hasBubble = false;
        typeId = 0;
        colorType = ColorType.None;
        isNeedFalling = false;
    }

    public void SetHasBubble(ColorType colorType, int id) {
        hasBubble = true;
        this.colorType = colorType;
        bubbleId = id;
    }
}