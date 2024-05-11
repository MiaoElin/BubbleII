using UnityEngine;

public static class GameBusiness_Result {

    public static void Tick(GameContext ctx, float dt) {
        int bubbleLen = ctx.bubbleRepo.TakeAll(out var allbubbles);

        // if (ctx.gameFsmCom.isEnteringResult) {
        //     ctx.gameFsmCom.isEnteringResult = false;
        //     if (ctx.gameFsmCom.isWin) {
        //         for (int i = 0; i < bubbleLen; i++) {
        //             var bubble = allbubbles[i];
        //             if (bubble == null) {
        //                 continue;
        //             }
        //             bubble.fsmCom.EnterFalling();
        //             bubble.landingPos = bubble.GetPos();
        //         }
        //     }

        // }

        // for (int i = 0; i < bubbleLen; i++) {
        //     var bubble = allbubbles[i];
        //     bubble.FallingEasing_Tick(dt);
        // }

    }
}