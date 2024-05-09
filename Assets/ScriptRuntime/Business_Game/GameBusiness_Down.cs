using UnityEngine;

public static class GameBusiness_Down {

    public static void Tick(GameContext ctx, float dt) {
        // int bubbleLen = ctx.bubbleRepo.TakeAll(out var allBubbles);
        // if (ctx.gameFsmCom.isEnteringDown) {
        //     ctx.gameFsmCom.isEnteringDown = false;
        //     for (int i = 0; i < bubbleLen; i++) {
        //         var bubble = allBubbles[i];
        //         if (bubble.fsmCom.status == BubbleStatus.Static) {
        //             float downOffset = Mathf.Sqrt(3) * GridConst.GridInsideRadius;
        //             bubble.downPos = bubble.GetPos();
        //             bubble.SetPos(bubble.GetPos() + Vector2.down * downOffset);
        //             Debug.Log(bubble.isDownEasing);
        //             bubble.isDownEasing = true;
        //         }
        //     }

        //     GridDomain.SpawnNewLine(ctx);
        //     return;
        // }

        // for (int i = 0; i < bubbleLen; i++) {
        //     var bubble = allBubbles[i];
        // }
    }
}