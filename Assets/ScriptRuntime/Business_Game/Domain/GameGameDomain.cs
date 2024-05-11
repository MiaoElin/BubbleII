using UnityEngine;

public static class GameGameDomain {

    public static void SetBubblePos_InGrid(GameContext ctx, BubbleEntity bubble) {
        // 找到最近格子
        bool has = GridDomain.FindNearlyGrid(ctx, bubble.landingPos, out var nearlyGrid);
        if (has) {
            // 设置Bubble位置
            bubble.SetPos(nearlyGrid.worldPos);
            bubble.fsmCom.EnterStatic();
            bubble.RemoveRigidboday();
            // 将格子设为有babbule
            nearlyGrid.SetHasBubble(bubble.colorType, bubble.id);
            // 搜索同色
            GridDomain.GridSearchColor(ctx, nearlyGrid.index);
        } else {
            Debug.LogError("There is no Grid Arround");
        }
    }

    public static void UnspawnSameColorBubble(GameContext ctx) {
        ctx.game.gridCom.Foreach(grid => {
            if (!grid.hasBubble) {
                return;
            }

            if (grid.hasSearchColor) {
                bool has = ctx.bubbleRepo.TryGet(grid.bubbleId, out var bubble);
                // 加分
                GameAddScore(ctx, bubble.score);
                // 销毁bubble
                BubbleDomain.Unspawn(ctx, bubble);
                // 重置grid
                grid.Reuse();
                // 播放消除vfx
                VFXDomain.BubbleBroke(ctx, bubble.GetPos());
            }

        });
    }

    public static void GameAddScore(GameContext ctx, int score) {
        ctx.game.score += score;
    }

    public static void UpspawnFallingBubble(GameContext ctx) {
        ctx.game.gridCom.Foreach(grid => {
            if (!grid.hasBubble || !grid.isNeedFalling) {
                return;
            }
            if (grid.isNeedFalling) {
                grid.isNeedFalling = false;
                bool has = ctx.bubbleRepo.TryGet(grid.bubbleId, out var bubble);
                // bubble进入缓动
                bubble.fsmCom.EnterFalling();
                bubble.fallingPos = bubble.GetPos();
                bubble.isFallingEasing = true;
                // 加分
                GameAddScore(ctx, bubble.score);
                // 重置grid
                grid.Reuse();
            }
        });
    }

    public static void BubbleDown(GameContext ctx) {
        var shootingBubble = ctx.shooter.shootingBubble;
        if (!shootingBubble) {
            Down(ctx);
            return;
        }
        var status = ctx.shooter.shootingBubble.fsmCom.status;
        if (status == BubbleStatus.Static) {
            Down(ctx);
        }
    }

    public static void Down(GameContext ctx) {
        if (ctx.shootCount > 0) {
            return;
        }
        ctx.shootCount = 4;
        // 生成一行新的
        GridDomain.SpawnNewline(ctx);

        // 遍历所有的泡泡,将不在Falling状态的泡泡都下移
        if (ctx.game.stage.currentFirstIndex > 0) {
            int bubbleLen = ctx.bubbleRepo.TakeAll(out var allBubbles);
            for (int i = 0; i < bubbleLen; i++) {
                var bubble = allBubbles[i];
                if (bubble.fsmCom.status == BubbleStatus.Falling) {
                    continue;
                }
                // 表现表现坐标，作为缓动起始
                bubble.srPos = bubble.GetPos();
                bubble.EnterDown();
                // 逻辑坐标直接修改
                float downOffset = Mathf.Sqrt(3) * GridConst.GridInsideRadius;
                bubble.SetPos(bubble.GetPos() + Vector2.down * downOffset);
            }
        }
        // 下降重置格子信息
        GridDomain.UpdateAllGrid(ctx);
    }
}