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
        LayerMask layerTop = 1 << 7; // top;
        LayerMask layerSide = 1 << 6;
        LayerMask layerBubble = 1 << 8;
        var hitTop1 = Physics2D.Raycast(VectorConst.ShooterPos, readyBubble1.faceDir, 100f, layerTop);
        var hitSide = Physics2D.Raycast(VectorConst.ShooterPos, readyBubble1.faceDir, 100f, layerSide);
        var hitBubble1 = Physics2D.Raycast(VectorConst.ShooterPos, readyBubble1.faceDir, 100f, layerBubble);

        if (hitBubble1) {
            Debug.Log(1);
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

            var hitTop2 = Physics2D.Raycast(hitSide.point, reflectDir, 100f, layerTop);
            var hitBubble2 = Physics2D.Raycast(hitSide.point, reflectDir, 100f, layerBubble);
            if (hitBubble2) {
                readyBubble1.landingPos = hitBubble2.point;
                shooter.SetLinePos(hitSide.point, hitBubble2.point);
            } else if (hitTop2) {
                readyBubble1.landingPos = hitTop2.point;
                shooter.SetLinePos(hitSide.point, hitTop2.point);
            }
        }

    }
}