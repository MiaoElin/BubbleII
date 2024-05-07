using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientMain : MonoBehaviour {
    [SerializeField] Canvas screenCanvas;
    GameContext ctx = new GameContext();
    bool isUnload = false;
    void Start() {

        // === Inject ===
        ctx.Inject(screenCanvas);

        // === Load ===
        Load();

        // === Bind ===
        EventBind();

        // === LoginEnter ===
        GameBusiness_Login.Enter(ctx);
    }

    private void EventBind() {
        var uiEevntCenter = ctx.uiApp.uIEventCenter;
        uiEevntCenter.OnStartClickHandle = () => {
            UIDomain.Panel_Login_Close(ctx);
        };
    }

    private void Load() {
        ctx.uiApp.LoadAll();
    }

    void Update() {


    }

    void OnApplicationQuit() {
        TearDown();
    }

    void OnDestory() {
        TearDown();
    }

    void TearDown() {
        if (isUnload) {
            return;
        }
        if (!isUnload) {
            UnLoad();
            isUnload = true;
        }
    }
    private void UnLoad() {
        ctx.uiApp.UnLoad();
    }
}
