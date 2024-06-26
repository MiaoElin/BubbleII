using System;
using UnityEngine;

public static class UIDomain {

    public static void Panel_Login_Open(GameContext ctx) {
        ctx.uiApp.Panel_Login_Open();
    }

    public static void Panel_Login_Close(GameContext ctx) {
        ctx.uiApp.Panel_Login_Close();
    }

    public static void Panel_GameStatus_Open(GameContext ctx) {
        ctx.uiApp.Panel_GameStatus_Open();
    }

    public static void Panel_GameStatus_Tick(GameContext ctx) {
        ctx.uiApp.Panel_GameStatus_Tick(ctx.game.score);
    }

    public static Panel_Win Panel_Win_Open(GameContext ctx) {
        return ctx.uiApp.Panel_Win_Open(ctx.game.stage.level, ctx.game.score);
    }

    public static void Panel_Win_EasingIn_Tick(GameContext ctx, float dt) {
        ctx.uiApp.Panel_Win_EasingIn_Tick(dt);
    }

    internal static void Panel_Win_Hide(GameContext ctx) {
        ctx.uiApp.Panel_Win_Hide();
    }
}