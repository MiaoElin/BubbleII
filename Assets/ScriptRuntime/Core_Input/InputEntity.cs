using UnityEngine;

public class InputEntity {

    public Vector2 mouseWorldPos;
    public Vector2 mouseScreenPos;
    public bool isMouseLeftDown;
    public void Process() {
        mouseScreenPos = Input.mousePosition;
        mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

        isMouseLeftDown = Input.GetMouseButtonDown(0);
    }
}