using UnityEngine;

public static class GridDomain {

    public static void Init(GameContext ctx) {
        var gridCom = ctx.game.gridCom;
        var stage = ctx.game.stage;
        // 生成格子
        gridCom.Ctor(stage.horizontalCount);
        var gridTypes = stage.gridTypes;
        for (int i = gridTypes.Length - GridConst.ScreenGridCount; i < gridTypes.Length; i++) {
            int index = i - (gridTypes.Length - GridConst.ScreenGridCount);
            gridCom.TryGetValue(index, out var grid);
            grid.typeId = gridTypes[i];
        }
    }

    public static bool FindNearlyGrid(GameContext ctx, Vector2 pos, out GridEntity nearlyGrid) {
        var gridCom = ctx.game.gridCom;
        return gridCom.FindNearlyGrid(pos, out nearlyGrid);
    }

}