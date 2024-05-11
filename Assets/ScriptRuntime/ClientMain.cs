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
            SFXDomain.BtnClick(ctx);
            // 打开ui
            UIDomain.Panel_Login_Close(ctx);
            GameBusiness_Normal.Enter(ctx);
        };

        uiEevntCenter.OnChangeClickHanle = () => {
            ShooterDomain.ChangeReadyBubble(ctx);
            // sfx
            SFXDomain.BtnClick(ctx);
        };
    }

    private void Load() {
        ctx.uiApp.LoadAll();
        ctx.asset.LoadAll();
        ctx.soundCore.LoadAll();
    }

    void Update() {
        float dt = Time.deltaTime;
        // var point = new Vector2(-15, -6);
        // Debug.Log(Camera.main.WorldToScreenPoint(point));

        // var point2 = new Vector2(160, 168);
        // Debug.Log((Vector2)Camera.main.ScreenToWorldPoint(point2));

        // var point3 = new Vector3(160, 168, 10);
        // Debug.Log(Camera.main.ScreenToWorldPoint(point3));

        ctx.input.Process();
        var status = ctx.gameFsmCom.status;
        if (status == GameStatus.Login) {
            GameBusiness_Login.Tick();
        } else if (status == GameStatus.Normal) {
            GameBusiness_Normal.Tick(ctx, dt);
        } else if (status == GameStatus.Result) {
            GameBusiness_Result.Tick(ctx, dt);
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
