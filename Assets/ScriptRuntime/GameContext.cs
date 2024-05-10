using UnityEngine;
using System.Collections.Generic;

public class GameContext {

    public float restSec;
    public int shootCount;

    public UIApp uiApp;

    // === Entity ===
    public GameEntity game;
    public InputEntity input;
    public BackSceneEntity backScene;
    public ShooterEntity shooter;

    // === Com ===
    public GameFSMComponet gameFsmCom;

    // === Core ===
    public Asset_Core asset;
    public SoundCore soundCore;

    public IDService iDService;

    // === Repo ===
    public BubbleRepo bubbleRepo;

    // === VFX ===
    public List<VFXEntity> vfxs;


    public GameContext() {
        shootCount = 4;
        uiApp = new UIApp();

        game = new GameEntity();
        input = new InputEntity();
        gameFsmCom = new GameFSMComponet();

        asset = new Asset_Core();
        soundCore = new SoundCore();

        bubbleRepo = new BubbleRepo();
        iDService = new IDService();

        vfxs = new List<VFXEntity>();
    }
    public void Inject(Canvas screenCanvas) {
        uiApp.Inject(screenCanvas);
    }
}