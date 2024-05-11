using UnityEngine;

public class GameFSMComponet {
    public GameStatus status;

    public bool isEnteringNormal;

    public bool isEnteringPause;

    public bool isEnteringResult;
    public bool isWin;

    public GameFSMComponet() {
        status = new GameStatus();
    }

    public void EnterNormal() {
        status = GameStatus.Normal;
        isEnteringNormal = true;
    }

    public void EnterResult(bool isWin) {
        status=GameStatus.Result;
        isEnteringResult = true;
        this.isWin = isWin;
    }
}