using UnityEngine;

public class GridEntity {

    public int index;
    public int typeId;
    public bool enable;
    public bool hasBubble;
    public int bubbleId;
    public ColorType colorType;
    public Vector2 worldPos;
    public Vector2Int viewPos;
    public Vector3Int coordinatePos;

    public bool inSingular;
    public bool hasSearchColor;

    public void Ctor(int index) {
        this.index = index;
        hasSearchColor = false;
    }

    public void Reuse() {
        hasBubble = false;
        colorType = ColorType.None;
    }

    public void SetHasBubble(ColorType colorType, int id) {
        hasBubble = true;
        this.colorType = colorType;
        bubbleId = id;
    }
}