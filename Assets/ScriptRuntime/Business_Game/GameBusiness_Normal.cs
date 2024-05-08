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
        ctx.shooter = ShooterDomain.Spawn(ctx);
        ctx.shooter.readyBubble1 = FakeBubbleDomain.Spawn(ctx, UnityEngine.Random.Range(1, 5), VectorConst.ShooterPos, VectorConst.scale1f);
        ctx.shooter.readyBubble2 = FakeBubbleDomain.Spawn(ctx, UnityEngine.Random.Range(1, 5), VectorConst.ReadyPos, VectorConst.scalehalf);

        ctx.gameFsmCom.EnterNormal();
    }

    public static void Tick(GameContext ctx, float dt) {

        PreTick(ctx, dt);

        ref var restSec = ref ctx.restSec;
        const float Interval = 0.01f;
        if (restSec < Interval) {
            FixedTick(ctx, restSec);
            restSec = 0;
        } else {
            while (restSec >= Interval) {
                restSec -= Interval;
                FixedTick(ctx, Interval);
            }
        }

        LateTick(ctx, dt);

    }
    public static void PreTick(GameContext ctx, float dt) {
        ShooterDomain.ShootLine(ctx);

        Physics2D.Simulate(dt);
    }

    public static void FixedTick(GameContext ctx, float fixdt) {


    }

    public static void LateTick(GameContext ctx, float dt) {

    }
}