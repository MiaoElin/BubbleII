using UnityEngine;

public static class VFXDomain {

    public static void VFXPlay(GameContext ctx, BubbleEntity bubble) {
        ctx.asset.TryGet_BubbleTM(bubble.typeId, out var tm);
        var vfx = GameFactory.CreateVFX(ctx, tm.vfx_BubbleBroke);
        vfx.transform.position = bubble.GetPos();
        ctx.vfxs.Add(vfx);
    }

    public static void Tick(GameContext ctx, VFXEntity vfx, float dt) {
        vfx.Tick(dt);
        if (vfx.isEnd) {
            Unspawn(ctx, vfx);
        }
    }

    public static void Unspawn(GameContext ctx, VFXEntity vfx) {
        ctx.vfxs.Remove(vfx);
        GameObject.Destroy(vfx.gameObject);
    }
}