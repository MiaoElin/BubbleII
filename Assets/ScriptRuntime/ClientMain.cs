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
            GameBusiness_Normal.Enter(ctx);
        };
    }

    private void Load() {
        ctx.uiApp.LoadAll();
        ctx.asset.LoadAll();
    }

    void Update() {
        float dt = Time.deltaTime;
        ctx.input.Process();
        var status = ctx.gameFsmCom.status;
        if (status == GameStatus.Login) {
            GameBusiness_Login.Tick();
        } else if (status == GameStatus.Normal) {
            GameBusiness_Normal.Tick(ctx, dt);
        }

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
        ctx.asset.Unload();
    }
}
