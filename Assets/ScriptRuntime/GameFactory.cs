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

}
