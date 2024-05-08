using UnityEngine;
using GameFunctions;

public class FakeBubbleEntity : MonoBehaviour {
    public int typeId;
    public SpriteRenderer sr;
    public Vector2 faceDir;
    public Vector2 reflectDir;
    public Vector2 landingPos;
    public Color color;

    public bool isMovingToShooterPos;
    float moveTimer;
    float moverDuration;


    public FakeBubbleEntity() {
        faceDir = new Vector2(0, 1);
        moveTimer = 0;
        moverDuration = 0.2f;
    }

    public void SetPos(Vector2 pos) {
        transform.position = pos;
    }

    public Vector2 GetPos() {
        return transform.position;
    }

    public void MoveByEasing(float dt) {
        moveTimer += dt;
        if (moveTimer >= moverDuration) {
            moveTimer = 0;
            isMovingToShooterPos = false;
        } else {
            transform.position = GFEasing.Ease2D(GFEasingEnum.Linear, moveTimer, moverDuration, VectorConst.ReadyPos, VectorConst.ShooterPos);
            transform.localScale = GFEasing.Ease2D(GFEasingEnum.Linear, moveTimer, moverDuration, VectorConst.scalehalf, VectorConst.scale1f);
        }
    }

}