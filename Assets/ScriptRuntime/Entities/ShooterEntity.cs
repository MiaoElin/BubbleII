using UnityEngine;

public class ShooterEntity : MonoBehaviour {

    public Vector2 shooterPos;
    public FakeBubbleEntity readyBubble1;
    public FakeBubbleEntity readyBubble2;
    public BubbleEntity shootingBubble;

    [SerializeField] LineRenderer lineR;

    public ShooterEntity() {
        shooterPos = VectorConst.ShooterPos;
    }

    public void SetLinREnable(bool b) {
        lineR.enabled = b;
    }

    public void SetLinePos(Vector2 point1, Vector2 point2) {
        lineR.SetPosition(0, VectorConst.ShooterPos);
        lineR.SetPosition(1, point1);
        lineR.SetPosition(2, point2);
    }
}