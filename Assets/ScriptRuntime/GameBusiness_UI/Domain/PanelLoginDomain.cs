using UnityEngine;

public static class Panel_LoginDomain {

    public static void Open(UIContext ctx) {
        var panel = GetPanel_Login(ctx);
        if (panel == null) {
            ctx.TryGet_UI_Prefab(typeof(Panel_Login).Name, out var prefab);
            panel = GameObject.Instantiate(prefab, ctx.screenCanvas.transform).GetComponent<Panel_Login>();
            panel.Ctor();
            panel.OnstarClickHandle = () => { ctx.uIEventCenter.Panel_Login_StartHanle(); };
            ctx.Add_UI(typeof(Panel_Login).Name, panel.gameObject);
        }
    }

    public static void Close(UIContext ctx) {
        var panel = GetPanel_Login(ctx);
        GameObject.Destroy(panel.gameObject);
    }

    public static Panel_Login GetPanel_Login(UIContext ctx) {
        return ctx.TryGet_UI<Panel_Login>();
    }
}