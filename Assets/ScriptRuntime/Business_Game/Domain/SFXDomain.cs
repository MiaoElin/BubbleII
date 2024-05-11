using System;
using UnityEngine;

public static class SFXDomain {

    public static void WinPlay(GameContext ctx) {
        ctx.soundCore.WinPlay(ctx.asset.configTM.sfx_win);
    }

    internal static void Bgm(GameContext ctx) {
        ctx.soundCore.BgmPlay(ctx.asset.configTM.bgm1);
    }

    internal static void BtnClick(GameContext ctx) {
        ctx.soundCore.BtnClick(ctx.asset.configTM.sfx_click);
    }

    internal static void BubbleShoot(GameContext ctx) {
        ctx.soundCore.BubbleShootPlay(ctx.asset.configTM.sfx_BubbleBroke);
    }
}