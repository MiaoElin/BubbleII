using UnityEngine;
using System.Collections.Generic;

public static class GridDomain {

    public static void Init(GameContext ctx) {
        var gridCom = ctx.game.gridCom;
        var stage = ctx.game.stage;
        // 生成格子
        gridCom.Ctor(stage.horizontalCount);
        SpawnAllGrid(ctx);
        // 初始化格子数据 
        var gridTypes = stage.gridTypes;
        for (int i = gridTypes.Length - GridConst.ScreenGridCount; i < gridTypes.Length; i++) {
            int index = i - (gridTypes.Length - GridConst.ScreenGridCount);
            var grid = gridCom.GetGrid(index);
            grid.typeId = gridTypes[i];
        }
    }

    public static void SpawnAllGrid(GameContext ctx) {
        var gridCom = ctx.game.gridCom;

        Vector2 gridBottom = VectorConst.GridBottom;
        float inRadius = GridConst.GridInsideRadius;
        float firstGridY = Mathf.Sqrt(3) * inRadius * (GridConst.ScreenVeticalCount - 1) + inRadius + gridBottom.y + 0.5f;
        float firstGridX1 = gridBottom.x - (2 * inRadius * (GridConst.ScreenHorizontalCount) / 2) + inRadius;
        float firstGridX2 = gridBottom.x - (2 * inRadius * (GridConst.ScreenHorizontalCount) / 2) + 2 * inRadius;
        // 生成格子
        for (int i = 0; i < GridConst.ScreenGridCount; i++) {
            var grid = new GridEntity();
            grid.Ctor(i);
            int x = GetX(i);
            int y = GetY(i);
            if (y % 2 == 1) {
                // 单行
                grid.worldPos.x = firstGridX2 + 2 * x * inRadius;
                if (x == GridConst.ScreenHorizontalCount - 1) {
                    grid.enable = false;
                    gridCom.SetGrid(grid);
                    continue;
                }
            } else {
                // 双行
                grid.worldPos.x = firstGridX1 + 2 * x * inRadius;
            }
            grid.enable = true;
            grid.worldPos.y = firstGridY - y * Mathf.Sqrt(3) * inRadius;
            gridCom.SetGrid(grid);
        }
    }

    public static int GetX(int index) {
        return index % GridConst.ScreenHorizontalCount;
    }

    public static int GetY(int index) {
        return index / GridConst.ScreenHorizontalCount;
    }

    public static int GetIndex_ByViewPos(int x, int y) {
        return y * GridConst.ScreenHorizontalCount + x;
    }

    public static int GetIndex_ByCoorPos(Vector3Int coordinatePos) {
        Vector2Int viewPos = GetViewPos(coordinatePos);
        return GetIndex_ByViewPos(viewPos.x, viewPos.y);
    }

    public static Vector2Int GetViewPos(Vector3Int coordinatePos) {
        int x = (coordinatePos.x + coordinatePos.y) / 2;
        int y = coordinatePos.z;
        return new Vector2Int(x, y);
    }

    public static Vector3Int GetCoordinatePos(Vector2Int viewPos) {
        int x = viewPos.x - viewPos.y / 2;
        int y = viewPos.y;
        int z = -x - y;
        return new Vector3Int(x, y, z);
    }

    public static Vector3Int GetCoordinatePos(int viewPosX, int viewPosY) {
        int x = viewPosX - viewPosY / 2;
        int y = viewPosY;
        int z = -x - y;
        return new Vector3Int(x, y, z);
    }

    public static bool FindNearlyGrid(GameContext ctx, Vector2 pos, out GridEntity nearlyGrid) {
        var gridCom = ctx.game.gridCom;
        return gridCom.FindNearlyGrid(pos, out nearlyGrid);
    }

    public static void GridSearchColor(GameContext ctx, int index) {
        var gridCom = ctx.game.gridCom;
        var grid = gridCom.GetGrid(index);
        grid.hasSearchColor = true;
        var temp = gridCom.temp;
        temp.Clear();
        temp.Add(grid.index);
        // 找周围的格子
        SearchColorArround(ctx, grid, temp);
        if (temp.Count < GridConst.SearchColorMinCount) {
            foreach (var id in temp) {
                var gri = gridCom.GetGrid(id);
                gri.hasSearchColor = false;
            }
        }
    }

    public static void SearchColorArround(GameContext ctx, GridEntity grid, List<int> temp) {
        // for (int j = -1; j <= 1; j++) {
        //     // 上行
        //     if (j == -1) {
        //         for (int i = 0; i <= 1; i++) {
        //             SearchColor(ctx, grid, i, j, ref temp);
        //         }
        //     } else if (j == 0) {
        //         for (int i = -1; i <= 1; i++) {
        //             if (i == 0) {
        //                 continue;
        //             }
        //             SearchColor(ctx, grid, i, j, ref temp);
        //         }
        //     } else if (j == 1) {
        //         for (int i = -1; i <= 0; i++) {
        //             SearchColor(ctx, grid, i, j, ref temp);
        //         }
        //     }
        // }
        var line = GetY(grid.index);
        bool isSingular = false;
        if (line % 2 == 1) {
            isSingular = true;
        }

        for (int j = -1; j <= 1; j++) {
            if (j == 0) {
                for (int i = -1; i <= 1; i++) {
                    if (i == 0) {
                        continue;
                    }
                    SearchColor(ctx, grid, i, j, temp);
                }
            } else {
                if (isSingular) {
                    for (int i = 0; i <= 1; i++) {
                        SearchColor(ctx, grid, i, j, temp);
                    }
                } else {
                    for (int i = -1; i <= 0; i++) {
                        SearchColor(ctx, grid, i, j, temp);
                    }
                }
            }
        }
    }

    public static void SearchColor(GameContext ctx, GridEntity centerGrid, int xOffset, int yOffset, List<int> temp) {
        // var gridCom = ctx.game.gridCom;
        // var horizontalCount = GridConst.ScreenHorizontalCount;
        // var VerticalCount = GridConst.ScreenVeticalCount;
        // int x = centerGrid.coordinatePos.x + xOffset;
        // int y = centerGrid.coordinatePos.y + yOffset;
        // Vector2 viewPos = GetViewPos(new Vector3Int(x, y, -x - y));
        // if (viewPos.x < 0 || viewPos.x > horizontalCount || viewPos.y < 0 || viewPos.y > VerticalCount) {
        //     return;
        // }
        // int newIndex = GetIndex_ByCoorPos(new Vector3Int(x, y, -x - y));
        // var newGrid = gridCom.GetGrid(newIndex);
        // if (!newGrid.hasBubble || newGrid.hasSearchColor) {
        //     return;
        // }
        // if (newGrid.colorType == centerGrid.colorType) {
        //     newGrid.hasSearchColor = true;
        //     temp.Add(newIndex);
        //     SearchColorArround(ctx, newGrid, ref temp);
        // }
        var gridCom = ctx.game.gridCom;
        var horizontalCount = GridConst.ScreenHorizontalCount;
        var VerticalCount = GridConst.ScreenVeticalCount;
        int x = GetX(centerGrid.index) + xOffset;
        int y = GetY(centerGrid.index) + yOffset;
        if (x < 0 || x >= horizontalCount || y < 0 || y >= VerticalCount) {
            return;
        }
        int index = GetIndex_ByViewPos(x, y);
        var grid = gridCom.GetGrid(index);
        if (!grid.hasBubble || grid.hasSearchColor) {
            return;
        }
        if (grid.colorType == centerGrid.colorType) {
            grid.hasSearchColor = true;
            temp.Add(index);
            SearchColorArround(ctx, grid, temp);
        }
    }
}