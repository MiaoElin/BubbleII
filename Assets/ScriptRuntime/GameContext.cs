using UnityEngine;

public class GameContext {

    public UIApp uiApp;

    public GameContext() {
        uiApp = new UIApp();
    }
    public void Inject(Canvas screenCanvas) {
        uiApp.Inject(screenCanvas);
    }
}