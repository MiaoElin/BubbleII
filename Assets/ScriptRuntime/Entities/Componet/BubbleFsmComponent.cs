using UnityEngine;
using System;
public class BubbleFsmComponent {

    public BubbleStatus status;

    public bool isShooting;
    public bool isArraived;
    public bool isStatic;
    public bool isFalling;
    public bool isDowing;



    public void EnterFalling() {
        status = BubbleStatus.Falling;
        isFalling = true;
    }

    public void EnterShooting() {
        status = BubbleStatus.Shooting;
        isShooting = true;
    }

    public void EnterArrived() {
        status = BubbleStatus.Arrived;
        isArraived = true;
    }

    public void EnterStatic() {
        status = BubbleStatus.Static;
        isStatic = true;
    }

    public void EnterDown() {
        status = BubbleStatus.Down;
        isDowing = true;
    }
}