using UnityEngine;

public static class FakeBubbleDomain {
    public static FakeBubbleEntity Spawn(GameContext ctx, int typeId, Vector2 pos, Vector2 scaleSize) {
        var fakeBubble = GameFactory.CreateFakeBubble(ctx, typeId, pos);
        fakeBubble.transform.localScale = scaleSize;
        return fakeBubble;
    }

    public static void Unspawn(FakeBubbleEntity fakeBubble) {
        GameObject.Destroy(fakeBubble.gameObject);
    }
}