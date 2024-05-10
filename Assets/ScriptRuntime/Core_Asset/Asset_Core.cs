using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Asset_Core {

    Dictionary<int, StageTM> allStageTMs;
    public AsyncOperationHandle stagePtr;

    Dictionary<int, BubbleTM> bubbleTMs;
    public AsyncOperationHandle bubblePtr;

    Dictionary<string, GameObject> entities;
    public AsyncOperationHandle entityPtr;

    Dictionary<int, FakeBubbleTM> fakeBubbleTMs;
    public AsyncOperationHandle fakeBubblePtr;

    public ConfigTM configTM;
    public AsyncOperationHandle configPtr;

    public Asset_Core() {
        allStageTMs = new Dictionary<int, StageTM>();
        bubbleTMs = new Dictionary<int, BubbleTM>();
        entities = new Dictionary<string, GameObject>();
        fakeBubbleTMs = new Dictionary<int, FakeBubbleTM>();
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
                bubbleTMs.Add(tm.typeId, tm);
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
        {
            var ptr = Addressables.LoadAssetsAsync<FakeBubbleTM>("FakeBubbleTM", null);
            fakeBubblePtr = ptr;
            var list = ptr.WaitForCompletion();
            foreach (var tm in list) {
                fakeBubbleTMs.Add(tm.typeId, tm);
            }
        }
        {
            var ptr = Addressables.LoadAssetAsync<ConfigTM>("ConfigTM");
            configPtr = ptr;
            var tm = ptr.WaitForCompletion();
            configTM = tm;
        }
    }

    public void Unload() {
        if (stagePtr.IsValid()) {
            Addressables.Release(stagePtr);
        }
        if (bubblePtr.IsValid()) {
            Addressables.Release(bubblePtr);
        }
        if (entityPtr.IsValid()) {
            Addressables.Release(entityPtr);
        }
        if (fakeBubblePtr.IsValid()) {
            Addressables.Release(fakeBubblePtr);
        }
        if (configPtr.IsValid()) {
            Addressables.Release(configPtr);
        }
    }

    public bool TryGet_StageTM(int typeId, out StageTM tm) {
        return allStageTMs.TryGetValue(typeId, out tm);
    }

    public bool TryGet_BubbleTM(int typeId, out BubbleTM tm) {
        return bubbleTMs.TryGetValue(typeId, out tm);
    }

    public bool TryGet_FakeBubbleTM(int typeId, out FakeBubbleTM tm) {
        return fakeBubbleTMs.TryGetValue(typeId, out tm);
    }

    public bool TryGet_Entity(string name, out GameObject prefab) {
        return entities.TryGetValue(name, out prefab);
    }

}