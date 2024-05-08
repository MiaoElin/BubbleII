using UnityEngine;

public class BubbleEntity : MonoBehaviour {

    public int id;
    public int typeId;
    public ColorType colorType;
    [SerializeField] public SpriteRenderer sr;
    [SerializeField] public Rigidbody2D rb;

    public Vector2 faceDir;
    public float moveSpeed;

    public bool isReflect;
    public Vector2 reflectDir;

    public BubbleFsmComponent fsmCom;

    public BubbleEntity() {
        fsmCom = new BubbleFsmComponent();
    }

    public void SetPos(Vector2 pos) {
        transform.position = pos;
    }

    public Vector2 GetPos() {
        return transform.position;
    }

    public void RemoveRigidboday() {
        DestroyImmediate(rb);
    }

    public void Move() {
        var velocity = rb.velocity;
        velocity = faceDir.normalized * moveSpeed;
        rb.velocity = velocity;
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "SideCollision") {
            isReflect = true;
        }
        if (other.gameObject.tag == "TopColliSion") {
            fsmCom.EnterArrived();
        }
        if (other.gameObject.tag == "BubbleEntity") {
            fsmCom.EnterArrived();
        }
    }
}