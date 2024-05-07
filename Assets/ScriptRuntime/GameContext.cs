using UnityEngine;

public class GameContext {

    public UIApp uiApp;

    // === Entity ===
    public GameEntity game;

    // === Com ===
    public GameFSMComponet gameFsmCom;

    // === Core ===
    public Asset_Core asset;

    public GameContext() {
        uiApp = new UIApp();
        game = new GameEntity();
        gameFsmCom = new GameFSMComponet();

        asset = new Asset_Core();
    }
    public void Inject(Canvas screenCanvas) {
        uiApp.Inject(screenCanvas);
    }
}