using UnityEngine;
using UnityEngine.UI;
using System;

public class Panel_Login : MonoBehaviour {

    [SerializeField] Button btn_Start;
    [SerializeField] Text title;
    public Action OnstarClickHandle;

    public void Ctor() {
        btn_Start.onClick.AddListener(() => {
            OnstarClickHandle.Invoke();
        });
    }

    internal void Close() {
        Destroy(gameObject);
    }
}