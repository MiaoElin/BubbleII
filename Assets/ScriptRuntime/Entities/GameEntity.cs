using UnityEngine;

public class GameEntity {
    public StageEntity stage;
    public GridComponet gridCom;
    public int score;

    public GameEntity() {
        stage = new StageEntity();
        gridCom = new GridComponet();
    }
}