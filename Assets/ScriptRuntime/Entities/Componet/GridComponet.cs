using UnityEngine;
using System;

public class GridComponet {

    GridEntity[] allGrid;
    int horizontalCount;

    public GridComponet() {

    }

    public void Ctor(int horizontalCount) {

        this.horizontalCount = horizontalCount;

        allGrid = new GridEntity[GridConst.ScreenGridCount];

        Vector2 gridBottom = VectorConst.GridBottom;
        float inRadius = GridConst.GridInsideRadius;
        float firstGridY = Mathf.Sqrt(3) * inRadius * (GridConst.ScreenVeticalCount - 1) + inRadius + gridBottom.y;
        float firstGridX1 = gridBottom.x - (2 * inRadius * (GridConst.ScreenHorizontalCount) / 2) + inRadius;
        float firstGridX2 = gridBottom.x - (2 * inRadius * (GridConst.ScreenHorizontalCount) / 2) + 2 * inRadius;
        // 生成格子
        for (int i = 0; i < allGrid.Length; i++) {
            var grid = new GridEntity();
            int x = GetX(i);
            int y = GetY(i);
            if (y % 2 == 1) {
                // 单行
                grid.worldPos.x = firstGridX2 + 2 * x * inRadius;
                grid.worldPos.y = firstGridY - y * Mathf.Sqrt(3) * inRadius;
                if (x == horizontalCount - 1) {
                    grid.enable = false;
                    allGrid[i] = grid;
                    continue;
                }
            } else {
                // 双行
                grid.worldPos.x = firstGridX1 + 2 * x * inRadius;
                grid.worldPos.y = firstGridY - y * Mathf.Sqrt(3) * inRadius;
            }
            grid.enable = true;
            grid.index = i;
            grid.coordinatePos = GetCoordinatePos(x, y);
            allGrid[i] = grid;
        }
    }

    public bool TryGetValue(int index, out GridEntity grid) {
        if (allGrid[index] != null) {
            grid = allGrid[index];
            return false;
        }
        grid = null;
        return true;
    }

    public int GetX(int index) {
        return index % horizontalCount;
    }

    public int GetY(int index) {
        return index / horizontalCount;
    }

    public int GetIndexByViewPos(int x, int y) {
        return y * horizontalCount + x;
    }

    public int GetIndexByCoorPos(Vector3Int coordinatePos) {
        Vector2Int viewPos = GetViewPos(coordinatePos);
        return GetIndexByViewPos(viewPos.x, viewPos.y);
    }

    public Vector2Int GetViewPos(Vector3Int coordinatePos) {
        int x = (coordinatePos.x + coordinatePos.y) / 2;
        int y = coordinatePos.z;
        return new Vector2Int(x, y);
    }

    public Vector3Int GetCoordinatePos(Vector2Int viewPos) {
        int x = viewPos.x - viewPos.y / 2;
        int y = viewPos.y;
        int z = -x - y;
        return new Vector3Int(x, y, z);
    }

    public Vector3Int GetCoordinatePos(int viewPosX, int viewPosY) {
        int x = viewPosX - viewPosY / 2;
        int y = viewPosY;
        int z = -x - y;
        return new Vector3Int(x, y, z);
    }

    public void Foreach(Action<GridEntity> action) {
        for (int i = 0; i < allGrid.Length; i++) {
            var grid = allGrid[i];
            action(grid);
        }
    }

    public bool FindNearlyGrid(Vector2 pos, out GridEntity nearlyGrid) {
        nearlyGrid = null;
        float nearlyDistance = 16;
        for (int i = 0; i < allGrid.Length; i++) {
            var grid = allGrid[i];
            if (grid.hasBubble || !grid.enable) {
                continue;
            }
            float distance = Vector2.SqrMagnitude(pos - grid.worldPos);
            if (distance <= nearlyDistance) {
                nearlyDistance = distance;
                nearlyGrid = grid;
            }
        }
        if (nearlyGrid == null) {
            return false;
        } else {
            return true;
        }
    }
}