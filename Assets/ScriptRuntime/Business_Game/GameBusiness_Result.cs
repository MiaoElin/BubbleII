using UnityEngine;

public static class GameBusiness_Result {

    public static void Tick(GameContext ctx, float dt) {
        int bubbleLen = ctx.bubbleRepo.TakeAll(out var allBubbles);

        if (ctx.gameFsmCom.isEnteringResult) {
            ctx.gameFsmCom.isEnteringResult = false;
            if (ctx.gameFsmCom.isWin) {
                // 掉落所有的bubble
                for (int i = 0; i < bubbleLen; i++) {
                    var bubble = allBubbles[i];
                    bubble.EnterFalling();
                    bubble.fallingPos = bubble.GetPos();
                    bubble.isFallingEasing = true;
                }
                var panel = UIDomain.Panel_Win_Open(ctx);
                UIDomain.Panel_Win_Hide(ctx);
                panel.isPanelEasingIn = true;
                // 播放胜利特效
                VFXDomain.Play(ctx, ctx.asset.configTM.vfx_Win, new Vector2(0, 0));
            }

        }

        // 下落缓动
        for (int i = 0; i < bubbleLen; i++) {
            var bubble = allBubbles[i];
            bubble.FallingEasing_Tick(dt);
        }

        // All vfx
        for (int i = 0; i < ctx.vfxs.Count; i++) {
            var vfx = ctx.vfxs[i];
            VFXDomain.Tick(ctx, vfx, dt);
        }

        if (ctx.gameFsmCom.isWin) {
            if (ctx.vfxs.Count == 0) {
                // 说明所有特效播放完了
                // 打开胜利页
                UIDomain.Panel_Win_Open(ctx);
                UIDomain.Panel_Win_EasingIn_Tick(ctx, dt);
            }
        }



    }
}