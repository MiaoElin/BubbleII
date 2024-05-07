using UnityEngine;

public class GridComponet {

    GridEntity[] allGrid;
    int horizontalCount;

    public GridComponet() {

    }

    public void Ctor(int horizontalCount) {

        this.horizontalCount = horizontalCount;

        allGrid = new GridEntity[GridConst.ScreenGridCount];

        Vector2 gridBottom = Vector2Const.GridBottom;
        float inRadius = GridConst.GridInsideRadius;
        float firstGridY = Mathf.Sqrt(3) * inRadius * (GridConst.ScreenVeticalCount - 1) + inRadius + gridBottom.y;
        float firstGridX = gridBottom.x - (2 * inRadius * (GridConst.ScreenHorizontalCount) / 2) + inRadius;
        // 生成格子
        for (int i = 0; i < allGrid.Length; i++) {
            var grid = allGrid[i];
            int x = GetX(i);
            int y = GetY(i);
            if (y % 2 == 1) {
                if (x == horizontalCount - 1) {
                    grid.enable = false;
                    allGrid[i] = grid;
                    continue;
                }
            }
            grid.enable = true;
            grid.index = i;
            grid.worldPos.x = firstGridX + 2 * x * inRadius;
            grid.worldPos.y = firstGridY - y * Mathf.Sqrt(3) * inRadius;
            grid.coordinatePos = GetCoordinatePos(x, y);
            allGrid[i] = grid;
        }
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

}