using UnityEngine;

public class ShooterEntity : MonoBehaviour {

    public Vector2 shootPos;
    public Vector2 readyPos;

    [SerializeField] LineRenderer lineR;

    public ShooterEntity() {
        shootPos = Vector2Const.ShooterPos;
        readyPos = Vector2Const.ReadyPos;
    }

    public void SetLinREnable(bool b) {
        lineR.enabled = b;
    }
    
    public void SetLine(Vector2 point1, Vector2 point2) {
        lineR.SetPosition(0, shootPos);
        lineR.SetPosition(1, point1);
        lineR.SetPosition(2, point2);
    }
}