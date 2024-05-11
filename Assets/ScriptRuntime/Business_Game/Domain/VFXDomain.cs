using UnityEngine;

public static class VFXDomain {

    public static void Play(GameContext ctx, Sprite[] sprs, Vector2 pos) {
        var vfx = GameFactory.CreateVFX(ctx, sprs);
        vfx.transform.position = pos;
        ctx.vfxs.Add(vfx);
    }

    public static void Tick(GameContext ctx, VFXEntity vfx, float dt) {
        vfx.Tick(dt);
        if (vfx.isEnd) {
            ctx.vfxs.Remove(vfx);
            Unspawn(ctx, vfx);
        }
    }

    public static void BubbleBroke(GameContext ctx, Vector2 pos) {
        Play(ctx, ctx.asset.configTM.vfx_BubbleBroke, pos);
    }

    public static void WinPlay(GameContext ctx) {
        Play(ctx, ctx.asset.configTM.vfx_Win, new Vector2(0, 0));
    }

    public static void Unspawn(GameContext ctx, VFXEntity vfx) {
        ctx.vfxs.Remove(vfx);
        GameObject.Destroy(vfx.gameObject);
    }
}