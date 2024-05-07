using System;
using UnityEngine;

public static class GameBusiness_Normal {

    public static void Enter(GameContext ctx) {

        var game = ctx.game;
        // 生成关卡
        game.stage = GameFactory.CreateStage(ctx, 0);

        // 生成格子
        game.gridCom.Ctor(game.stage.horizontalCount);


        ctx.gameFsmCom.EnterNormal();
    }

    public static void Tick() {

    }
}