using UnityEngine;

public static class BubbleDomain {

    public static BubbleEntity Spawn(GameContext ctx, int typeId, Vector2 pos) {
        var bubble = GameFactory.CreateBubble(ctx, typeId, pos);
        ctx.bubbleRepo.Add(bubble);
        return bubble;
    }

    public static BubbleEntity SpawnStatic(GameContext ctx, int typeId, Vector2 pos) {
        var bubble = Spawn(ctx, typeId, pos);
        bubble.RemoveRigidboday();
        return bubble;
    }

    public static void Unspawn(GameContext ctx, BubbleEntity bubble) {
        ctx.bubbleRepo.Remove(bubble);
        GameObject.Destroy(bubble.gameObject);
    }

    public static void Move(BubbleEntity bubble) {
        // if (bubble.fsmCom.status == BubbleStatus.Shooting) {
        if (bubble.isReflect) {
            bubble.isReflect = false;
            bubble.faceDir = bubble.reflectDir;
            bubble.Move();
        } else {
            bubble.Move();
        }
        // }
    }

    public static void SetStatic(BubbleEntity bubble) {
        bubble.fsmCom.EnterStatic();
        bubble.RemoveRigidboday();
    }

    public static void FallingEasing_Tick(BubbleEntity bubble, float dt) {
        bubble.FallingEasing_Tick(dt);
    }

}