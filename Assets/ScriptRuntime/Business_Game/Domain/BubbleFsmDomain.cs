using System;
using UnityEngine;

public static class BubbleFsmDomain {

    public static void ApplyFsm(GameContext ctx, BubbleEntity shootingBubble, float dt) {
        if (!shootingBubble) {
            return;
        }
        var status = shootingBubble.fsmCom.status;
        if (status == BubbleStatus.Shooting) {
            BubbleDomain.Move(shootingBubble,dt);
        } else if (status == BubbleStatus.Arrived) {
            GameGameDomain.SetBubblePos_InGrid(ctx, shootingBubble);
        }
    }
}