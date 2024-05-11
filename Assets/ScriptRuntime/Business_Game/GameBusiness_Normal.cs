using System;
using UnityEngine;

public static class GameBusiness_Normal {

    public static void Enter(GameContext ctx) {
        // 生成背景
        ctx.backScene = GameFactory.CreateBackScene(ctx);

        // 打开UI
        UIDomain.Panel_GameStatus_Open(ctx);

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
        const float IntervalTime = 0.01f;
        restSec += dt;
        if (restSec < IntervalTime) {
            FixedTick(ctx, restSec);
            restSec = 0;
        } else {
            while (restSec >= IntervalTime) {
                restSec -= IntervalTime;
                FixedTick(ctx, IntervalTime);
            }
        }

        LateTick(ctx, dt);

    }
    public static void PreTick(GameContext ctx, float dt) {

    }

    public static void ApplyResult(GameContext ctx) {
        var game = ctx.game;
        if (game.score >= game.stage.targetCore) {
            // 关闭射线
            ctx.shooter.SetLinREnable(false);
            // 进入胜利页
            ctx.gameFsmCom.EnterResult(true);
        }
    }
    public static void FixedTick(GameContext ctx, float dt) {

        // 发射射线
        ShooterDomain.ShootLine(ctx);

        // 发射泡泡
        ShooterDomain.ShootBubble(ctx);

        // 泡泡整体下降
        GameGameDomain.BubbleDown(ctx);

        // ShootingBubble Fsm
        BubbleFsmDomain.ApplyFsm(ctx, ctx.shooter.shootingBubble, dt);

        // 消除同色的泡泡
        GameGameDomain.UnspawnSameColorBubble(ctx);

        // 消除掉落泡泡
        GridDomain.UpdateFaling(ctx);
        GameGameDomain.UpspawnFallingBubble(ctx);

        Physics2D.Simulate(dt);

    }

    public static void LateTick(GameContext ctx, float dt) {
        // 发射器缓动
        ShooterDomain.Easing_Tick(ctx, dt);

        // 下落缓动
        int bubbleLen = ctx.bubbleRepo.TakeAll(out var allBubbles);
        for (int i = 0; i < bubbleLen; i++) {
            var bubble = allBubbles[i];
            if (bubble.fsmCom.status == BubbleStatus.Falling) {
                BubbleDomain.FallingEasing_Tick(bubble, dt);
            } else if (bubble.fsmCom.status == BubbleStatus.Down) {
                BubbleDomain.DownEasing_Tick(bubble, dt);
            }
        }

        // All vfx
        for (int i = 0; i < ctx.vfxs.Count; i++) {
            var vfx = ctx.vfxs[i];
            VFXDomain.Tick(ctx, vfx, dt);
        }

        // 计分
        UIDomain.Panel_GameStatus_Tick(ctx);
        // 结果结算 
        ApplyResult(ctx);

        // ctx.game.gridCom.Foreach(grid => {
        //     if (grid.hasBubble) {
        //     Debug.DrawLine(grid.worldPos, grid.worldPos + Vector2.down * 1, Color.red);
        //     }
        // });
    }
}