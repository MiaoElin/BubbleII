using UnityEngine;

public class VFXEntity : MonoBehaviour {
    [SerializeField] SpriteRenderer sr;
    public Sprite[] allsprite;
    int currentIndex;
    float timer;
    bool isEnd;

    public void Ctor(Sprite[] allsprite) {
        this.allsprite = allsprite;
        currentIndex = 0;
        timer = 0;
        isEnd = false;
        sr.sprite = allsprite[currentIndex];
    }

    public void Tick(float dt) {
        timer += dt;
        if (timer >= GameConst.ANIMATOR_INTERVAL) {
            timer -= GameConst.ANIMATOR_INTERVAL;
            currentIndex++;
            if (currentIndex >= allsprite.Length) {
                isEnd = true;
            } else {
                sr.sprite = allsprite[currentIndex];
            }
        }
    }
}