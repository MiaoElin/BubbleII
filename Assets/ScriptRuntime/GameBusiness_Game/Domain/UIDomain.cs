using UnityEngine;

public static class UIDomain {

    public static void Panel_Login_Open(GameContext ctx) {
        ctx.uiApp.Panel_Login_Open();
    }

    public static void Panel_Login_Close(GameContext ctx) {
        ctx.uiApp.Panel_Login_Close();
    }
}