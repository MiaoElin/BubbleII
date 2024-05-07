using UnityEngine;
using System.Collections.Generic;

public class BubbleRepo {

    public Dictionary<int, BubbleEntity> all;
    BubbleEntity[] temp;

    public BubbleRepo() {
        all = new Dictionary<int, BubbleEntity>();
        temp = new BubbleEntity[200];
    }

    public void Add(BubbleEntity bubble) {
        all.Add(bubble.id, bubble);
    }

    public bool TryGet(int id, out BubbleEntity value) {
        return all.TryGetValue(id, out value);
    }

    public void Remove(BubbleEntity bubble) {
        all.Remove(bubble.id);
    }

    public int TakeAll(out BubbleEntity[] allbubble) {
        if (all.Count > temp.Length) {
            temp = new BubbleEntity[all.Count * 2];
        }
        all.Values.CopyTo(temp, 0);
        allbubble = temp;
        return all.Count;
    }
}