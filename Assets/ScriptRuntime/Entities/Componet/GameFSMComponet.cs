using UnityEngine;

public class GameFSMComponet {
    public GameStatus status;

    public bool isEnteringNormal;

    public bool isEnteringPause;

    public GameFSMComponet() {
        status = new GameStatus();
    }

    public void EnterNormal() {
        status = GameStatus.Normal;
        isEnteringNormal = true;
    }
}