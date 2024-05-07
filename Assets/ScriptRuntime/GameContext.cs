using UnityEngine;

public class GameContext {

    public UIApp uiApp;

    // === Entity ===
    public GameEntity game;

    // === Com ===
    public GameFSMComponet gameFsmCom;

    public GameContext() {
        uiApp = new UIApp();
        game = new GameEntity();
        gameFsmCom = new GameFSMComponet();
    }
    public void Inject(Canvas screenCanvas) {
        uiApp.Inject(screenCanvas);
    }
}