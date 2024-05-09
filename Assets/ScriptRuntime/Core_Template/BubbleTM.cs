using UnityEngine;

[CreateAssetMenu(menuName = "TM/BubbleTM", fileName = "BubbleTM_")]
public class BubbleTM : ScriptableObject {
    public int typeId;
    public int score;
    public ColorType colorType;
    public Sprite spr;
    public float moveSpeed;
    public Sprite[] vfx_BubbleBroke;
}

