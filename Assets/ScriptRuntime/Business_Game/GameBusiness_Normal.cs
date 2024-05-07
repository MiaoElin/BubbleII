using System;
using UnityEngine;

public static class GameBusiness_Normal {

    public static void Enter(GameContext ctx) {
        // 生成背景
        ctx.backScene = GameFactory.CreateBackScene(ctx);

        // 生成关卡
        ctx.game.stage = GameFactory.CreateStage(ctx, 0);

        // 初始化格子
        GridDomain.Init(ctx);

        // 生成Bubble
        var gridCom = ctx.game.gridCom;
        gridCom.Foreach(grid => {
            if (!grid.enable || grid.typeId == 0) {
                return;
            }
            var bubble = BubbleDomain.SpawnStatic(ctx, grid.typeId, grid.worldPos);
            grid.SetHasBubble(bubble.colorType, bubble.id);
        });

        // 生成发射器
        ctx.shooter = GameFactory.CreateShooter(ctx);


        ctx.gameFsmCom.EnterNormal();
    }

    public static void Tick() {

    }
}