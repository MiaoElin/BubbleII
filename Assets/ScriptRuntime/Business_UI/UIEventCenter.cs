using UnityEngine;
using System;

public class UIEventCenter {

    public Action OnStartClickHandle;
    public void Panel_Login_StartHanle() { OnStartClickHandle.Invoke(); }

    public Action OnChangeClickHanle;
    public void Panel_GameStatus_ChangeHandle() { OnChangeClickHanle.Invoke(); }
}