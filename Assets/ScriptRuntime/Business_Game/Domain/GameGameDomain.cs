using UnityEngine;

public static class GameGameDomain {

    public static void UnspawnSameColorBubble(GameContext ctx) {
        ctx.game.gridCom.Foreach(grid => {
            if (!grid.hasBubble || !grid.hasSearchColor) {
                return;
            }
            bool has = ctx.bubbleRepo.TryGet(grid.bubbleId, out var bubble);
            VFXDomain.VFXPlay(ctx, bubble);
            BubbleDomain.Unspawn(ctx, bubble);
            grid.Reuse();
        });
    }

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
}