using UnityEngine;

public static class GameFactory {
    public static StageEntity CreateStage(GameContext ctx, int typeId) {
        bool has = ctx.asset.TryGet_StageTM(typeId, out var tm);
        if (!has) {
            Debug.LogError($"GameFactory.CreateStage {typeId} is not Find");
        }
        StageEntity stage = new StageEntity();
        stage.typeId = typeId;
        stage.level = tm.level;
        stage.horizontalCount = tm.horizontalCount;
        stage.VerticalCount = tm.VerticalCount;
        stage.gridTypes = tm.girdTypes;
        return stage;
    }

    public static BubbleEntity CreateBubble(GameContext ctx, int typeId, Vector2 pos) {
        bool has = ctx.asset.TryGet_BubbleTM(typeId, out var tm);
        if (!has) {
            Debug.LogError($"GameFactory.CreateBubble {typeId} is not find");
        }

        ctx.asset.TryGet_Entity(typeof(BubbleEntity).Name, out var prefab);
        BubbleEntity bubble = GameObject.Instantiate(prefab).GetComponent<BubbleEntity>();
        bubble.typeId = typeId;
        bubble.colorType = tm.colorType;
        bubble.sr.sprite = tm.spr;
        bubble.id = ctx.iDService.bubbleRecord++;
        bubble.SetPos(pos);
        return bubble;
    }

    public static FakeBubbleEntity CreateFakeBubble(GameContext ctx, int typeId, Vector2 pos) {
        bool has = ctx.asset.TryGet_FakeBubbleTM(typeId, out var tm);
        if (!has) {
            Debug.LogError($"GameFactory.CreateFakeBubble {typeId} is not find");
        }
        ctx.asset.TryGet_Entity(typeof(FakeBubbleEntity).Name, out var prefab);
        FakeBubbleEntity fakeBubble = GameObject.Instantiate(prefab).GetComponent<FakeBubbleEntity>();
        fakeBubble.typeId = typeId;
        fakeBubble.sr.sprite = tm.spr;
        fakeBubble.color = tm.color;
        fakeBubble.SetPos(pos);
        return fakeBubble;
    }

    public static BackSceneEntity CreateBackScene(GameContext ctx) {
        bool has = ctx.asset.TryGet_Entity(typeof(BackSceneEntity).Name, out var prefab);
        var backScene = GameObject.Instantiate(prefab).GetComponent<BackSceneEntity>();
        return backScene;
    }

    public static ShooterEntity CreateShooter(GameContext ctx) {
        ctx.asset.TryGet_Entity(typeof(ShooterEntity).Name, out var prefab);
        var shooter = GameObject.Instantiate(prefab).GetComponent<ShooterEntity>();
        return shooter;
    }

}
