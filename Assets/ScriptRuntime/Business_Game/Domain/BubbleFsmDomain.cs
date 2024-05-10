using System;
using UnityEngine;

public static class BubbleFsmDomain {

    public static void ApplyFsm(GameContext ctx, BubbleEntity shootingBubble, float dt) {
        if (!shootingBubble) {
            return;
        }
        var status = shootingBubble.fsmCom.status;
        if (status == BubbleStatus.Static) {
            ApplyStatic(ctx, shootingBubble, dt);
        } else if (status == BubbleStatus.Shooting) {
            ApplyShooting(ctx, shootingBubble, dt);
        } else if (status == BubbleStatus.Arrived) {
            GameGameDomain.SetBubblePos_InGrid(ctx, shootingBubble);
        }
    }

    private static void ApplyStatic(GameContext ctx, BubbleEntity shootingBubble, float dt) {
        var fsmCom = shootingBubble.fsmCom;
        // 发射bubble
        if (ctx.shootCount <= 0) {
            ctx.shootCount = 4;
            // 生成一行新的
            GridDomain.SpawnNewline(ctx);
            if (ctx.game.stage.currentFirstIndex > 0) {
                int bubbleLen = ctx.bubbleRepo.TakeAll(out var allBubbles);
                for (int i = 0; i < bubbleLen; i++) {
                    var bubble = allBubbles[i];
                    if (bubble.fsmCom.status != BubbleStatus.Falling) {
                        float downOffset = Mathf.Sqrt(3) * GridConst.GridInsideRadius;
                        bubble.downPos = bubble.GetPos();
                        bubble.SetPos(bubble.GetPos() + Vector2.down * downOffset);
                        bubble.EnterDown();
                    }
                }
            }
            // 下降重置格子
            GridDomain.UpdateGrid(ctx);
        }
    }

    private static void ApplyShooting(GameContext ctx, BubbleEntity shootingBubble, float dt) {
        BubbleDomain.Move(shootingBubble);
    }
}