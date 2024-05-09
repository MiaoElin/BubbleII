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

    public bool isChangeEasing;
    float changeTimer;
    float changeDuration;

    public FakeBubbleEntity() {
        faceDir = new Vector2(0, 1);
        moveTimer = 0;
        moverDuration = 0.2f;

        changeTimer = 0;
        changeDuration = 0.2f;
    }

    public void SetPos(Vector2 pos) {
        transform.position = pos;
    }

    public Vector2 GetPos() {
        return transform.position;
    }

    public void MoveByEasing_Tick(float dt) {
        if (!isMovingToShooterPos) {
            return;
        }
        moveTimer += dt;
        if (moveTimer >= moverDuration) {
            moveTimer = 0;
            isMovingToShooterPos = false;
        } else {
            transform.position = GFEasing.Ease2D(GFEasingEnum.Linear, moveTimer, moverDuration, VectorConst.ReadyPos, VectorConst.ShooterPos);
            transform.localScale = GFEasing.Ease2D(GFEasingEnum.Linear, moveTimer, moverDuration, VectorConst.scalehalf, VectorConst.scale1f);
        }
    }

    public void ChangePosEasing(float dt, bool thisFakeInLeft) {
        if (!isChangeEasing) {
            return;
        }
        changeTimer += dt;
        if (changeTimer >= changeDuration) {
            isChangeEasing = false;
            changeTimer = 0;
        } else {
            if (thisFakeInLeft) {
                transform.position = GFEasing.Ease2D(GFEasingEnum.Linear, changeTimer, changeDuration, VectorConst.ReadyPos, VectorConst.ShooterPos);
                transform.localScale = GFEasing.Ease2D(GFEasingEnum.Linear, changeTimer, changeDuration, VectorConst.scalehalf, VectorConst.scale1f);
            } else {
                transform.position = GFEasing.Ease2D(GFEasingEnum.Linear, changeTimer, changeDuration, VectorConst.ShooterPos, VectorConst.ReadyPos);
                transform.localScale = GFEasing.Ease2D(GFEasingEnum.Linear, changeTimer, changeDuration, VectorConst.scale1f, VectorConst.scalehalf);
            }

        }
    }




}