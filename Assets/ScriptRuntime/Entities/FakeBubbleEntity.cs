using UnityEngine;

public class FakeBubbleEntity : MonoBehaviour {
    public int typeId;
    public SpriteRenderer sr;
    public Vector2 faceDir;
    public Vector2 landingPos;
    public FakeBubbleEntity() {
        faceDir = new Vector2(0, 1);
    }
    public void SetPos(Vector2 pos) {
        transform.position = pos;
    }
}