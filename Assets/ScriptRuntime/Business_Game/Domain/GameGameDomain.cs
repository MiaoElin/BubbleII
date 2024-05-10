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
                // 播放消除vfx
                VFXDomain.VFXPlay(ctx, bubble);
                // 加分
                GameAddScore(ctx, bubble.score);
                // 销毁bubble
                BubbleDomain.Unspawn(ctx, bubble);
                // 重置grid
                grid.Reuse();
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
                bool has = ctx.bubbleRepo.TryGet(grid.bubbleId, out var bubble);
                // bubble进入缓动
                bubble.fsmCom.EnterFalling();
                bubble.fallingPos = bubble.GetPos();
                bubble.isFallingEasing = true;
                // jiafe
                GameAddScore(ctx, bubble.score);
                // 重置grid
                grid.Reuse();
            }
        });
    }
}