using UnityEngine;

public static class ShooterDomain {

    public static ShooterEntity Spawn(GameContext ctx) {
        var shooter = GameFactory.CreateShooter(ctx);
        return shooter;
    }

    public static void ShootLine(GameContext ctx) {
        var shooter = ctx.shooter;
        var readyBubble1 = shooter.readyBubble1;
        shooter.SetLinREnable(true);
        shooter.SetlineColor(readyBubble1.color);
        readyBubble1.faceDir = ctx.input.mouseWorldPos - VectorConst.ShooterPos;
        var shootPos = VectorConst.ShooterPos;
        LayerMask layerTop = 1 << 7; // top;
        LayerMask layerSide = 1 << 6;
        LayerMask layerBubble = 1 << 8;
        var hitTop1 = Physics2D.Raycast(shootPos, readyBubble1.faceDir, 100f, layerTop);
        var hitSide = Physics2D.Raycast(shootPos, readyBubble1.faceDir, 100f, layerSide);
        var hitBubble1 = Physics2D.Raycast(shootPos, readyBubble1.faceDir, 100f, layerBubble);

        if (hitBubble1) {
            readyBubble1.landingPos = hitBubble1.point;
            shooter.SetLinePos(hitBubble1.point);
        } else if (hitTop1) {
            readyBubble1.landingPos = hitTop1.point;
            shooter.SetLinePos(hitTop1.point);
        } else if (hitSide) {
            Vector2 reflectDir;
            var faceDir = readyBubble1.faceDir;
            // 要转动的角度
            float angle = Mathf.Atan(faceDir.x / faceDir.y);
            // 向量逆时针旋转
            reflectDir = new Vector2(-Mathf.Sin(angle), Mathf.Cos(angle));
            readyBubble1.reflectDir = reflectDir;

            var hitTop2 = Physics2D.Raycast(hitSide.point, reflectDir, 100f, layerTop);
            var hitBubble2 = Physics2D.Raycast(hitSide.point, reflectDir, 100f, layerBubble);
            var hitSide2 = Physics2D.Raycast(hitSide.point, reflectDir, 100f, layerSide);
            if (hitBubble2) {
                readyBubble1.landingPos = hitBubble2.point;
                shooter.SetLinePos(hitSide.point, hitBubble2.point);
            } else if (hitTop2) {
                readyBubble1.landingPos = hitTop2.point;
                shooter.SetLinePos(hitSide.point, hitTop2.point);
            } else if (hitSide2) {
                // Debug.Log("hit2" + hitSide2.point);
                // Debug.DrawRay(hitSide.point, reflectDir * 5f, Color.red);
                // readyBubble1.landingPos = hitSide2.point;
                // shooter.SetLinePos(hitSide.point, hitSide2.point);
            }
        }
    }

    public static void ShootBubble(GameContext ctx) {
        ref var ShootingBubble = ref ctx.shooter.shootingBubble;
        if (ShootingBubble && ShootingBubble.fsmCom.status != BubbleStatus.Static) {
            return;
        }
        if (ctx.input.isMouseLeftDown && ctx.input.isMouseInGrid) {
            ctx.shootCount--;
            Debug.Log(ctx.shootCount);
            ref var readyBubble1 = ref ctx.shooter.readyBubble1;
            ref var readyBubble2 = ref ctx.shooter.readyBubble2;
            // 根据readybubble1生成bubble
            ShootingBubble = BubbleDomain.Spawn(ctx, readyBubble1.typeId, VectorConst.ShooterPos);
            ShootingBubble.faceDir = readyBubble1.faceDir;
            ShootingBubble.reflectDir = readyBubble1.reflectDir;
            ShootingBubble.landingPos = readyBubble1.landingPos;
            ShootingBubble.fsmCom.EnterShooting();

            // 销毁readyBubble1 将readyBubble2赋值给 readyBubble1
            FakeBubbleDomain.Unspawn(readyBubble1);
            var fakeBubble = readyBubble2;
            readyBubble1 = fakeBubble;
            readyBubble1.GetComponentInChildren<SpriteRenderer>().sortingOrder = 100;
            readyBubble1.isMovingToShooterPos = true;

            // 生成新的 readyBubble2
            readyBubble2 = FakeBubbleDomain.Spawn(ctx, UnityEngine.Random.Range(1, 5), VectorConst.ReadyPos, VectorConst.scalehalf);
            readyBubble2.GetComponentInChildren<SpriteRenderer>().sortingOrder = 99;

            // 播放sfx
            SFXDomain.BubbleShoot(ctx);
        }
    }

    public static void ChangeReadyBubble(GameContext ctx) {
        ref var readyBubble1 = ref ctx.shooter.readyBubble1;
        ref var readyBubble2 = ref ctx.shooter.readyBubble2;
        var bubble2 = readyBubble2;

        // 交换
        readyBubble2 = readyBubble1;
        readyBubble1 = bubble2;

        //Bubble1的层级在上，也要修改成上
        readyBubble2.GetComponentInChildren<SpriteRenderer>().sortingOrder = 99;
        readyBubble1.GetComponentInChildren<SpriteRenderer>().sortingOrder = 100;

        readyBubble1.isChangeEasing = true;
        readyBubble2.isChangeEasing = true;


    }

    public static void Easing_Tick(GameContext ctx, float dt) {
        var readyBubble1 = ctx.shooter.readyBubble1;
        var readyBubble2 = ctx.shooter.readyBubble2;
        readyBubble1.MoveByEasing_Tick(dt);
        readyBubble1.ChangePosEasing(dt, true);
        readyBubble2.ChangePosEasing(dt, false);
    }
}