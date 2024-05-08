using UnityEngine;

public class InputEntity {

    public Vector2 mouseWorldPos;
    public Vector2 mouseScreenPos;
    public void Process() {
        mouseScreenPos = Input.mousePosition;
        mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }
}