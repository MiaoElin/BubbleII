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
}