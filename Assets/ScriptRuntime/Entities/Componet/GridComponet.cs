using UnityEngine;
using System;
using System.Collections.Generic;

public class GridComponet {

    GridEntity[] allGrid;
    public List<GridEntity> searchColorTemp;
    public List<GridEntity> searchTractionTemp;
    public bool isSpawnNewLine;



    public GridComponet() {

    }

    public void Ctor() {
        allGrid = new GridEntity[GridConst.ScreenGridCount];
        searchColorTemp = new List<GridEntity>();
        searchTractionTemp = new List<GridEntity>();
    }

    public GridEntity GetGrid(int index) {
        return allGrid[index];
    }

    public void SetGrid(GridEntity grid) {
        allGrid[grid.index] = grid;
    }

    public void Foreach(Action<GridEntity> action) {
        for (int i = 0; i < allGrid.Length; i++) {
            var grid = allGrid[i];
            action(grid);
        }
    }

    public void Foreach_Reverse(Action<GridEntity> action) {
        for (int i = allGrid.Length - 1; i >= 0; i--) {
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