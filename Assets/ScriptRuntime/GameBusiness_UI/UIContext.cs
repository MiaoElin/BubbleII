using UnityEngine;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;

public class UIContext {
    Dictionary<string, GameObject> uiPrefabs;
    public AsyncOperationHandle uiptr;
    Dictionary<string, GameObject> allUI;
    public Canvas screenCanvas;
    public UIEventCenter uIEventCenter;

    public UIContext() {
        uiPrefabs = new Dictionary<string, GameObject>();
        allUI = new Dictionary<string, GameObject>();
        uIEventCenter = new UIEventCenter();
    }

    public void Add_UI_Prefab(string name, GameObject prefab) {
        uiPrefabs.Add(name, prefab);
    }

    public bool TryGet_UI_Prefab(string name, out GameObject prefab) {
        return uiPrefabs.TryGetValue(name, out prefab);
    }

    public void Add_UI(string name, GameObject value) {
        allUI.Add(name, value);
    }

    public T TryGet_UI<T>() where T : MonoBehaviour {

        bool has = allUI.TryGetValue(typeof(T).Name, out GameObject value);
        if (has) {
            return value.GetComponent<T>();
        } else {
            return null;
        }
    }
}