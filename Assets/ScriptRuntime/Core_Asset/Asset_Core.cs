using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Asset_Core {

    Dictionary<int, StageTM> allStageTM;
    public AsyncOperationHandle stagePtr;

    public Asset_Core() {
        allStageTM = new Dictionary<int, StageTM>();
    }

    public void Load() {
        {
            var ptr = Addressables.LoadAssetsAsync<StageTM>("StageTM", null);
            stagePtr = ptr;
            var list = ptr.WaitForCompletion();
            foreach (var tm in list) {
                allStageTM.Add(tm.typeId, tm);
            }
        }
    }

    public void Unload() {
        if (stagePtr.IsValid()) {
            Addressables.Release(stagePtr);
        }
    }

    public bool TryGet_StageTM(int typeId, out StageTM tm) {
        return allStageTM.TryGetValue(typeId, out tm);
    }
}