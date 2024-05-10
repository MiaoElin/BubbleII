using System;
using UnityEngine;

public static class BubbleFsmDomain {

    public static void ApplyFsm(GameContext ctx, BubbleEntity bubble, float dt) {
        var status = bubble.fsmCom.status;
        if (status == BubbleStatus.Shooting) {

        } else if (status == BubbleStatus.Falling) {
            ApplyFalling(ctx, bubble, dt);
        }
    }

    private static void ApplyFalling(GameContext ctx, BubbleEntity bubble, float dt) {
        var fsmCom = bubble.fsmCom;
        if (fsmCom.isFalling) {
            fsmCom.isFalling = false;
        }

    }
}