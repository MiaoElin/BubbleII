using UnityEngine;

public static class PanelGameStatusDomain {

    public static void Open(UIContext ctx) {
        var panel = ctx.TryGet_UI<Panel_GameStatus>();
        if (panel == null) {
            ctx.TryGet_UI_Prefab(typeof(Panel_GameStatus).Name, out var prefab);
            panel = GameObject.Instantiate(prefab, ctx.screenCanvas.transform).GetComponent<Panel_GameStatus>();
            panel.Ctor();
            panel.OnChangeCickHandle = () => {
                ctx.uIEventCenter.Panel_GameStatus_ChangeHandle();
            };
            ctx.Add_UI(typeof(Panel_GameStatus).Name, panel.gameObject);
        }
    }

    public static void Panel_GameStatus_Close(UIContext ctx) {
        var panel = ctx.TryGet_UI<Panel_GameStatus>();
        panel?.gameObject.SetActive(false);
    }

    public static void Tick(UIContext ctx, int score) {
        var panel = ctx.TryGet_UI<Panel_GameStatus>();
        panel?.Tick(score);
    }
}