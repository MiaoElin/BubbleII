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
        // 发射射线
        ShooterDomain.ShootLine(ctx);

        // 发射bubble
        ShooterDomain.ShootBubble(ctx);

        // 移动ShootingBubble
        var shootingBubble = ctx.shooter.shootingBubble;
        if (shootingBubble) {
            var fsmCom = ctx.shooter.shootingBubble.fsmCom;
            if (fsmCom.status == BubbleStatus.Shooting) {
                BubbleDomain.Move(shootingBubble);
            } else if (fsmCom.status == BubbleStatus.Arrived) {
                GameGameDomain.SetBubblePos_InGrid(ctx, shootingBubble);
            }
        }

        // 消除同色的泡泡
        GameGameDomain.UnspawnSameColorBubble(ctx);

        // 消除掉落泡泡


        Physics2D.Simulate(dt);
    }

    public static void FixedTick(GameContext ctx, float fixdt) {


    }

    public static void LateTick(GameContext ctx, float dt) {
        var readyBubble1 = ctx.shooter.readyBubble1;
        if (readyBubble1.isMovingToShooterPos) {
            readyBubble1.MoveByEasing(dt);
        }

        for (int i = 0; i < ctx.vfxs.Count; i++) {
            var vfx = ctx.vfxs[i];
            VFXDomain.Tick(ctx, vfx, dt);
        }
    }
}