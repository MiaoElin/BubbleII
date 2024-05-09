using UnityEngine;

public class InputEntity {

    public Vector2 mouseWorldPos;
    public Vector2 mouseScreenPos;
    public bool isMouseLeftDown;
    public bool isMouseInGrid;
    public void Process() {
        mouseScreenPos = Input.mousePosition;
        mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

        isMouseLeftDown = Input.GetMouseButtonDown(0);
        isMouseInGrid = PureFuction.IsPosInRect(mouseWorldPos, VectorConst.GridRectLeftBottom, VectorConst.GridSize);
    }
}