using UnityEngine;

public class BubbleEntity : MonoBehaviour {

    public int id;
    public int typeId;
    public ColorType colorType;
    [SerializeField] public SpriteRenderer sr;
    [SerializeField] Rigidbody2D rb;
    public BubbleEntity() {

    }

    public void SetPos(Vector2 pos) {
        transform.position = pos;
    }

    public void RemoveRigidboday() {
        DestroyImmediate(rb);
    }
}