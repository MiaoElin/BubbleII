using UnityEngine;
using System.Collections.Generic;

public static class GridDomain {

    public static void Init(GameContext ctx) {
        var gridCom = ctx.game.gridCom;
        var stage = ctx.game.stage;
        // 生成格子
        gridCom.Ctor();
        SpawnAllGrid(ctx);
        // 初始化格子数据:遍历第一个格子到最后一个格子
        var gridTypes = stage.gridTypes;
        int firstgrid = gridTypes.Count - GridConst.ScreenGridCount;
        for (int i = firstgrid; i < gridTypes.Count; i++) {
            int index = i - (firstgrid) + GridConst.ScreenHorizontalCount; // 第一个为15；
            var grid = gridCom.GetGrid(index);
            grid.typeId = gridTypes[i];
        }
        for (int i = gridTypes.Count - 1; i >= firstgrid; i--) {
            gridTypes.RemoveAt(i);
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
        for (int i = 0; i < GridConst.GridCount; i++) {
            var grid = new GridEntity();
            int x = GetX(i);
            int y = GetY(i);
            grid.Ctor(i, new Vector2Int(x, y));
            if (y % 2 == 0) {
                // 单行
                grid.isinSingular = true;
                grid.worldPos.x = firstGridX2 + 2 * x * inRadius;
                grid.worldPos.y = firstGridY - y * Mathf.Sqrt(3) * inRadius;
                if (x == GridConst.ScreenHorizontalCount - 1) {
                    grid.enable = false;
                    gridCom.SetGrid(grid);
                    continue;
                }
            } else {
                // 双行
                grid.isinSingular = false;
                grid.worldPos.x = firstGridX1 + 2 * x * inRadius;
                grid.worldPos.y = firstGridY - y * Mathf.Sqrt(3) * inRadius;
            }
            grid.enable = true;
            grid.worldPos.y = firstGridY - y * Mathf.Sqrt(3) * inRadius;
            gridCom.SetGrid(grid);
        }
    }

    public static void SpawnNewline(GameContext ctx) {
        var gridCom = ctx.game.gridCom;
        var stage = ctx.game.stage;
        var gridTypes = stage.gridTypes;
        ref var currentFirstIndex = ref stage.currentFirstIndex;
        if (currentFirstIndex <= 0) {
            return;
        }
        for (int i = currentFirstIndex - GridConst.ScreenHorizontalCount; i < currentFirstIndex; i++) {
            int index = i - (currentFirstIndex - GridConst.ScreenHorizontalCount);
            var typeId = gridTypes[i];
            var grid = gridCom.GetGrid(index);
            if (grid.enable && typeId != 0) {
                grid.typeId = typeId;
                var bubble = BubbleDomain.SpawnStatic(ctx, typeId, grid.worldPos);
                grid.SetHasBubble(bubble.colorType, bubble.id);
            }
        }

    }


    public static void UpdateGrid(GameContext ctx) {
        var gridCom = ctx.game.gridCom;
        var horizontalCount = GridConst.ScreenHorizontalCount;
        var stage = ctx.game.stage;
        var gridTypes = stage.gridTypes;
        ref var currentFirstIndex = ref stage.currentFirstIndex;
        if (currentFirstIndex <= 0) {
            return;
        }
        // 移除stage里的
        int minCount = gridTypes.Count - GridConst.ScreenHorizontalCount;

        for (int i = gridTypes.Count - 1; i >= minCount; i--) {
            gridTypes.RemoveAt(i);
        }
        currentFirstIndex -= GridConst.ScreenHorizontalCount;
        Debug.Log(currentFirstIndex);


        gridCom.Foreach_Reverse(grid => {
            if (grid.index > 134) {
                if (grid.hasBubble) {
                    // todo 输了
                }
                return;
            }
            Debug.Log(grid.isinSingular);
            int newindex = grid.index + horizontalCount;
            var newGrid = gridCom.GetGrid(newindex);
            ResetGrid(newGrid, grid);
        });

        // 修改第一排
        gridCom.Foreach(grid => {
            if (grid.index >= horizontalCount) {
                return;
            }
            grid.Reuse();
            ReverseGrid(grid);

            if (grid.index == horizontalCount - 1) {
                if (grid.isinSingular) {
                    grid.enable = false;
                } else {
                    grid.enable = true;
                }
            }
        });

    }

    public static void ResetGrid(GridEntity newGrid, GridEntity oldGrid) {
        if (oldGrid.isinSingular) {
            newGrid.isinSingular = true;
            // 右移
            newGrid.worldPos = newGrid.worldPos + Vector2.right * GridConst.GridInsideRadius;

        } else {
            newGrid.isinSingular = false;
            newGrid.worldPos += Vector2.left * GridConst.GridInsideRadius;

        }
        newGrid.hasBubble = oldGrid.hasBubble;
        newGrid.bubbleId = oldGrid.bubbleId;
        newGrid.colorType = oldGrid.colorType;
        newGrid.enable = oldGrid.enable;
        newGrid.typeId = oldGrid.typeId;
        newGrid.hasSearchColor = false;
        newGrid.hasSearchTraction = false;
    }

    #region Search Color
    public static void GridSearchColor(GameContext ctx, int index) {
        var gridCom = ctx.game.gridCom;
        var grid = gridCom.GetGrid(index);
        grid.hasSearchColor = true;
        var temp = gridCom.searchColorTemp;
        temp.Clear();
        temp.Add(grid);
        // 找周围的格子
        SearchColorArround(ctx, grid, temp);
        if (temp.Count < GridConst.SearchColorMinCount) {
            foreach (var gri in temp) {
                gri.hasSearchColor = false;
            }
        }
    }

    public static void SearchColorArround(GameContext ctx, GridEntity grid, List<GridEntity> temp) {
        var line = GetY(grid.index);
        // bool isSingular = false;
        // if (line % 2 == 1) {
        //     isSingular = true;
        // }

        for (int j = -1; j <= 1; j++) {
            if (j == 0) {
                for (int i = -1; i <= 1; i++) {
                    if (i == 0) {
                        continue;
                    }
                    SearchColor(ctx, grid, i, j, temp);
                }
            } else {
                if (grid.isinSingular) {
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
    public static void SearchColor(GameContext ctx, GridEntity centerGrid, int xOffset, int yOffset, List<GridEntity> temp) {
        var gridCom = ctx.game.gridCom;
        var horizontalCount = GridConst.ScreenHorizontalCount;
        var VerticalCount = GridConst.ScreenVeticalCount;
        int x = GetX(centerGrid.index) + xOffset;
        int y = GetY(centerGrid.index) + yOffset;
        if (x < 0 || x >= horizontalCount || y < 0 || y >= VerticalCount) {
            return;
        }
        int index = GetIndex(x, y);
        var grid = gridCom.GetGrid(index);
        if (!grid.hasBubble || grid.hasSearchColor) {
            return;
        }
        if (grid.colorType == centerGrid.colorType) {
            grid.hasSearchColor = true;
            temp.Add(grid);
            SearchColorArround(ctx, grid, temp);
        }
    }
    #endregion

    #region Search Falling
    public static void UpdateFaling(GameContext ctx) {
        var gridCom = ctx.game.gridCom;
        gridCom.Foreach(grid => {
            if (!grid.hasBubble || grid.index < GridConst.ScreenHorizontalCount * 2) {
                return;
            }
            // 预设为true
            var temp = gridCom.searchTractionTemp;
            temp.Clear();
            temp.Add(grid);

            grid.isNeedFalling = true;
            grid.hasSearchTraction = true;
            // 搜索周围
            SearchArroundTraction(ctx, grid.index, grid, temp);
            foreach (var gri in temp) {
                gri.hasSearchTraction = false;
            }
            // if (!grid.isNeedFalling) {
            // } else {

            // }

        });
    }

    public static void SearchArroundTraction(GameContext ctx, int index, GridEntity centerGrid, List<GridEntity> temp) {
        var gridCom = ctx.game.gridCom;
        // bool isSingular = false;
        // int line = GetY(index);
        // if (line % 2 == 1) {
        //     isSingular = true;
        // }
        var grid = gridCom.GetGrid(index);
        for (int j = -1; j <= 1; j++) {
            if (j == 0) {
                for (int i = -1; i <= 1; i++) {
                    if (i == 0) {
                        continue;
                    }
                    SearchTraction(ctx, index, centerGrid, i, j, temp);
                    if (!centerGrid.isNeedFalling) {
                        break;
                    }
                }
            } else {
                if (j == -1) {
                }
                if (grid.isinSingular) {
                    for (int i = 0; i <= 1; i++) {
                        SearchTraction(ctx, index, centerGrid, i, j, temp);
                        if (!centerGrid.isNeedFalling) {
                            break;
                        }
                    }
                } else {
                    for (int i = -1; i <= 0; i++) {
                        SearchTraction(ctx, index, centerGrid, i, j, temp);
                        if (!centerGrid.isNeedFalling) {
                            break;
                        }
                    }
                }
            }
        }
    }
    public static void SearchTraction(GameContext ctx, int index, GridEntity centerGrid, int xOffset, int yOffset, List<GridEntity> temp) {
        var gridCom = ctx.game.gridCom;
        var horizontalCount = GridConst.ScreenHorizontalCount;
        var VerticalCount = GridConst.ScreenVeticalCount;
        int x = GetX(index) + xOffset;
        int y = GetY(index) + yOffset;
        if (x < 0 || x >= horizontalCount || y < 0 || y >= VerticalCount) {
            return;
        }
        int newindex = GetIndex(x, y);
        var newGrid = gridCom.GetGrid(newindex);
        if (!newGrid.hasBubble || newGrid.hasSearchTraction) {
            return;
        }
        if (y == 1) {
            centerGrid.isNeedFalling = false;
            return;
        } else {
            newGrid.hasSearchTraction = true;
            temp.Add(newGrid);
            // 搜索周围
            SearchArroundTraction(ctx, newindex, centerGrid, temp);
        }
    }
    #endregion

    // public static void SpawnNewLine(GameContext ctx) {
    //     var gridCom = ctx.game.gridCom;
    //     if (!gridCom.isSpawnNewLine) {
    //         return;
    //     }
    //     gridCom.isSpawnNewLine = false;
    //     var gridTypes = ctx.game.stage.gridTypes;
    //     int horizontalCount = GridConst.ScreenHorizontalCount;
    //     if (gridTypes.Count - horizontalCount < 0) {
    //         return;
    //     }


    //     gridCom.Foreach_Reverse(grid => {
    //         if (grid.index > 119) {
    //             return;
    //         }
    //         int newGridIndex = grid.index + horizontalCount;
    //         if (newGridIndex > GridConst.GridIndexMax) {
    //             // 游戏输了 todo
    //             return;
    //         }
    //         var newGrid = gridCom.GetGrid(newGridIndex);
    //         ResetGrid(newGrid, grid);
    //     });

    //     for (int i = gridTypes.Count - 1; i >= gridTypes.Count - horizontalCount; i--) {
    //         var typeId = gridTypes[i];
    //         // 第一个为0；随着i减小增d大
    //         int index = (gridTypes.Count - 1) - i;
    //         // 将0到horizontal个设置bubble
    //         var grid = gridCom.GetGrid(index);
    //         grid.Reuse();
    //         ReverseGrid(grid);
    //         grid.typeId = typeId;

    //         if (index == horizontalCount - 1) {
    //             if (grid.isinSingular) {
    //                 grid.enable = false;
    //             } else {
    //                 grid.enable = true;
    //             }
    //         }
    //         if (grid.enable && grid.typeId != 0) {
    //             var bubble = BubbleDomain.SpawnStatic(ctx, typeId, grid.worldPos);
    //             grid.SetHasBubble(bubble.colorType, bubble.id);
    //         }
    //     }

    //     int minCount = gridTypes.Count - horizontalCount;

    //     for (int i = gridTypes.Count - 1; i >= minCount; i--) {
    //         gridTypes.RemoveAt(i);
    //     }

    // }

    public static void ReverseGrid(GridEntity grid) {
        if (grid.isinSingular) {
            grid.isinSingular = false;
            grid.worldPos += Vector2.left * GridConst.GridInsideRadius;
        } else {
            grid.isinSingular = true;
            grid.worldPos += Vector2.right * GridConst.GridInsideRadius;
        }
    }


    public static int GetX(int index) {
        return index % GridConst.ScreenHorizontalCount;
    }

    public static int GetY(int index) {
        return index / GridConst.ScreenHorizontalCount;
    }

    public static int GetIndex(int x, int y) {
        return y * GridConst.ScreenHorizontalCount + x;
    }

    public static bool FindNearlyGrid(GameContext ctx, Vector2 pos, out GridEntity nearlyGrid) {
        var gridCom = ctx.game.gridCom;
        return gridCom.FindNearlyGrid(pos, out nearlyGrid);
    }
}