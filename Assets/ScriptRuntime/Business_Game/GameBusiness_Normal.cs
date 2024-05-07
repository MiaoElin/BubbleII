using System;
using UnityEngine;

public static class GameBusiness_Normal {

    public static void Enter(GameContext ctx) {
        ctx.gameFsmCom.EnterNormal();
    }

    public static void Tick() {
    }
}