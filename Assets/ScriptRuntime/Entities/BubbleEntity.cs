using UnityEngine;
using GameFunctions;

public class BubbleEntity : MonoBehaviour {

    public int id;
    public int typeId;
    public int score;
    public ColorType colorType;
    [SerializeField] public SpriteRenderer sr;
    [SerializeField] public Rigidbody2D rb;

    public Vector2 faceDir;
    public float moveSpeed;

    public bool isReflect;
    public Vector2 reflectDir;
    public Vector2 landingPos;

    // === FSM ===
    public BubbleFsmComponent fsmCom;

    // === 掉落缓动 ===
    public bool isFallingEasing;
    public float falling_timer;
    public float falling_MounDuration;
    public float falling_Duration;
    public Vector2 fallingPos;

    // === 下移缓动 ===
    public bool isDownEasing;
    public float down_timer;
    public float down_Duration;
    public Vector2 srPos;

    public BubbleEntity() {
        fsmCom = new BubbleFsmComponent();
    }

    public void Ctor() {
        falling_timer = 0;
        falling_MounDuration = 0.3f;
        falling_Duration = 0.4f;

        down_timer = 0;
        down_Duration = 0.4f;
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
        if (other.gameObject.tag == "TopCollision") {
            fsmCom.EnterArrived();
        }
        if (other.gameObject.tag == "BubbleEntity") {
            fsmCom.EnterArrived();
        }
    }

    // public void FallingEasing_Tick(float dt) {
    //     fsmCom.FallingEasing_Tick(dt, () => {
    //         sr.transform.position=GFEasing.Ease2D(GFEasingEnum.MountainInCirc,)
    //     });
    // }
    public void FallingEasing_Tick(float dt) {
        if (!isFallingEasing) {
            return;
        }
        falling_timer += dt;
        if (falling_timer <= falling_MounDuration) {
            sr.transform.position = GFEasing.Ease2D(GFEasingEnum.MountainInCirc, falling_timer, falling_MounDuration, fallingPos, new Vector2(fallingPos.x, fallingPos.y + 3));
        } else if (falling_timer <= falling_Duration) {
            sr.transform.position = GFEasing.Ease2D(GFEasingEnum.Linear, falling_timer, falling_MounDuration, fallingPos, fallingPos + Vector2.down * 15);
        } else {
            isFallingEasing = false;
            falling_timer = 0;
            Destroy(gameObject);
        }
    }

    public void EnterDown() {
        fsmCom.EnterDown();
        isDownEasing = true;
    }

    public void EnterStatic() {
        fsmCom.EnterStatic();
    }

    public void DownEasing_Tick(float dt) {
        if (!isDownEasing) {
            return;
        }
        down_timer += dt;
        if (down_timer <= down_Duration) {
            sr.transform.position = GFEasing.Ease2D(GFEasingEnum.Linear, down_timer, down_Duration, srPos, GetPos());
        } else {
            isDownEasing = false;
            down_timer = 0;
            EnterStatic();
        }
    }

}