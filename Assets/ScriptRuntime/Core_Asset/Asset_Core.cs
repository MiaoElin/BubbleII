using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Asset_Core {

    Dictionary<int, StageTM> allStageTMs;
    public AsyncOperationHandle stagePtr;

    Dictionary<int, BubbleTM> allBubbleTMs;
    public AsyncOperationHandle bubblePtr;

    Dictionary<string, GameObject> entities;
    public AsyncOperationHandle entityPtr;

    public Asset_Core() {
        allStageTMs = new Dictionary<int, StageTM>();
        allBubbleTMs = new Dictionary<int, BubbleTM>();
        entities = new Dictionary<string, GameObject>();
    }

    public void LoadAll() {
        {
            var ptr = Addressables.LoadAssetsAsync<StageTM>("StageTM", null);
            stagePtr = ptr;
            var list = ptr.WaitForCompletion();
            foreach (var tm in list) {
                allStageTMs.Add(tm.typeId, tm);
            }
        }
        {
            var ptr = Addressables.LoadAssetsAsync<BubbleTM>("BubbleTM", null);
            bubblePtr = ptr;
            var list = ptr.WaitForCompletion();
            foreach (var tm in list) {
                allBubbleTMs.Add(tm.typeId, tm);
            }
        }
        {
            var ptr = Addressables.LoadAssetsAsync<GameObject>("Entities", null);
            entityPtr = ptr;
            var list = ptr.WaitForCompletion();
            foreach (var prefab in list) {
                entities.Add(prefab.name, prefab);
            }
        }
    }

    public void Unload() {
        if (stagePtr.IsValid()) {
            Addressables.Release(stagePtr);
        }

        if (bubblePtr.IsValid()) {
            Addressables.Release(bubblePtr);
        }
    }

    public bool TryGet_StageTM(int typeId, out StageTM tm) {
        return allStageTMs.TryGetValue(typeId, out tm);
    }

    public bool TryGet_bubbleTM(int typeId, out BubbleTM tm) {
        return allBubbleTMs.TryGetValue(typeId, out tm);
    }

    public bool TryGet_Entity(string name, out GameObject prefab) {
        return entities.TryGetValue(name, out prefab);
    }
}