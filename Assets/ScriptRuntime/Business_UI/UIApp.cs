using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
public class UIApp {

    UIContext ctx;
    public UIEventCenter uIEventCenter => ctx.uIEventCenter;

    public UIApp() {
        ctx = new UIContext();
    }

    public void Inject(Canvas screenCanvas) {
        ctx.screenCanvas = screenCanvas;
    }

    public void LoadAll() {
        {
            var ptr = Addressables.LoadAssetsAsync<GameObject>("UI", null);
            var list = ptr.WaitForCompletion();
            ctx.uiptr = ptr;
            foreach (var prefab in list) {
                ctx.Add_UI_Prefab(prefab.name, prefab);
            }
        }
    }

    public void UnLoad() {
        if (ctx.uiptr.IsValid()) {
            Addressables.Release(ctx.uiptr);
        }
    }

    public void Panel_Login_Open() {
        Panel_LoginDomain.Open(ctx);
    }

    public void Panel_Login_Close() {
        Panel_LoginDomain.Close(ctx);
    }

    public void Panel_GameStatus_Open() {
        PanelGameStatusDomain.Open(ctx);
    }

    public void Panel_GameStatus_Tick(int score) {
        PanelGameStatusDomain.Tick(ctx, score);
    }

    public Panel_Win Panel_Win_Open(int stageLevel, int score) {
        var panel = ctx.TryGet_UI<Panel_Win>();
        if (panel == null) {
            ctx.TryGet_UI_Prefab(typeof(Panel_Win).Name, out var prefab);
            panel = GameObject.Instantiate(prefab, ctx.screenCanvas.transform).GetComponent<Panel_Win>();
            panel.Ctor();
            ctx.Add_UI(typeof(Panel_Win).Name, panel.gameObject);
        }
        panel.Init(stageLevel, score);
        panel.gameObject.SetActive(true);
        return panel;
    }

    public void Panel_Win_Hide() {
        var paenl = ctx.TryGet_UI<Panel_Win>();
        paenl?.Hide();
    }

    public void Panel_Win_EasingIn_Tick(float dt) {
        var Panel = ctx.TryGet_UI<Panel_Win>();
        Panel?.EasingIn_Tick(dt);
    }
}