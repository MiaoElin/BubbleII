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
            // sfx
            ctx.soundCore.BtnClick(ctx.asset.configTM.sfx_click);
            // 打开ui
            UIDomain.Panel_Login_Close(ctx);
            GameBusiness_Normal.Enter(ctx);
        };

        uiEevntCenter.OnChangeClickHanle = () => {
            ShooterDomain.ChangeReadyBubble(ctx);
            // sfx
            ctx.soundCore.BtnClick(ctx.asset.configTM.sfx_click);
        };
    }

    private void Load() {
        ctx.uiApp.LoadAll();
        ctx.asset.LoadAll();
        ctx.soundCore.LoadAll();
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
        ctx.soundCore.Unload();
    }
}
