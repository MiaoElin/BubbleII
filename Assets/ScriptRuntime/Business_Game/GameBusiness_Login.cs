using System;
using UnityEngine;

public static class GameBusiness_Login {

    public static void Enter(GameContext ctx) {
        UIDomain.Panel_Login_Open(ctx);
        var clip = ctx.asset.configTM.bgm1;
        ctx.soundCore.BgmPlay(clip);
    }

    internal static void Tick() {
        throw new NotImplementedException();
    }
}