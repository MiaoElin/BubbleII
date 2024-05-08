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

        var shootingBubble = ctx.shooter.shootingBubble;
        // 移动ShootingBubble
        if (shootingBubble) {
            var fsmCom = ctx.shooter.shootingBubble.fsmCom;
            if (fsmCom.status == BubbleStatus.Shooting) {
                BubbleDomain.Move(shootingBubble);
            } else if (fsmCom.status == BubbleStatus.Arrived) {

                // 找到最近格子
                bool has = GridDomain.FindNearlyGrid(ctx, shootingBubble.GetPos(), out var nearlyGrid);
                if (has) {
                        
                    // 设置Bubble位置
                    shootingBubble.SetPos(nearlyGrid.worldPos);
                    fsmCom.EnterStatic();
                    shootingBubble.RemoveRigidboday();

                    // 将格子设为有格子
                    nearlyGrid.SetHasBubble(shootingBubble.colorType, shootingBubble.id);
                } else {
                    Debug.LogError("There is no Grid Arround");
                }
            }
        }



        Physics2D.Simulate(dt);
    }

    public static void FixedTick(GameContext ctx, float fixdt) {


    }

    public static void LateTick(GameContext ctx, float dt) {
        var readyBubble1 = ctx.shooter.readyBubble1;
        if (readyBubble1.isMovingToShooterPos) {
            readyBubble1.MoveByEasing(dt);
        }
    }
}