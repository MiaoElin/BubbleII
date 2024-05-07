using UnityEngine;

public class GameContext {

    public UIApp uiApp;

    // === Entity ===
    public GameEntity game;
    public BackSceneEntity backScene;
    public ShooterEntity shooter;

    // === Com ===
    public GameFSMComponet gameFsmCom;

    // === Core ===
    public Asset_Core asset;

    public IDService iDService;

    // === Repo ===
    public BubbleRepo bubbleRepo;

    public GameContext() {
        uiApp = new UIApp();
        game = new GameEntity();
        gameFsmCom = new GameFSMComponet();

        asset = new Asset_Core();

        bubbleRepo = new BubbleRepo();
        iDService = new IDService();
    }
    public void Inject(Canvas screenCanvas) {
        uiApp.Inject(screenCanvas);
    }
}