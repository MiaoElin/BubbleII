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
    public float moveTimer;
    public float moverDuration;

    public bool isChangeEasing;
    public float changeTimer;
    public float changeDuration;

    public FakeBubbleEntity() {
        faceDir = new Vector2(0, 1);
        moveTimer = 0;
        moverDuration = 0.1f;

        changeTimer = 0;
        changeDuration = 0.1f;
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
        if (moveTimer > moverDuration) {
            moveTimer = 0;
            isMovingToShooterPos = false;
        } else {
            moveTimer += dt;
            transform.position = GFEasing.Ease2D(GFEasingEnum.Linear, moveTimer, moverDuration, VectorConst.ReadyPos, VectorConst.ShooterPos);
            transform.localScale = GFEasing.Ease2D(GFEasingEnum.Linear, moveTimer, moverDuration, VectorConst.scalehalf, VectorConst.scale1f);
        }
    }

    public void ChangePosEasing(float dt, bool thisFakeInLeft) {
        if (!isChangeEasing) {
            return;
        }
        if (changeTimer > changeDuration) {
            // if (thisFakeInLeft) {
            //     transform.position = VectorConst.ShooterPos;
            // } else {
            //     transform.position = VectorConst.ReadyPos;
            // }
            isChangeEasing = false;
            changeTimer = 0;
        } else {
            changeTimer += dt;
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