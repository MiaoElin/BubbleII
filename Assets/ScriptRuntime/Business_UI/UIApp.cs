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

}