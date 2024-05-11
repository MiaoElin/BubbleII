using System;
using UnityEngine.UI;
using UnityEngine;
using GameFunctions;

public class Panel_Win : MonoBehaviour {
    [SerializeField] Text title;
    [SerializeField] Text txt_Stage;
    [SerializeField] Text txt_Score;
    [SerializeField] Button btn_NextStage;
    [SerializeField] Button btn_Restart;
    [SerializeField] Button btn_BackMenu;
    public Action OnNectStageHandle;
    public Action OnRestartHandle;
    public Action OnBackMenuHandle;

    public bool isPanelEasingIn;
    public float timer;
    public float duration;
    public Vector2 startPos;
    public Vector2 endPos;

    public Panel_Win() {
        startPos = new Vector2(0, 460);
        endPos = new Vector2(0, 0);
        timer = 0;
        duration = 1;
        isPanelEasingIn = false;
    }

    public void Ctor() {
        btn_NextStage.onClick.AddListener(() => {
            OnNectStageHandle.Invoke();
        });

        btn_Restart.onClick.AddListener(() => {
            OnRestartHandle.Invoke();
        });

        btn_BackMenu.onClick.AddListener(() => {
            OnBackMenuHandle.Invoke();
        });

    }

    public void Init(int stageLevel, int score) {
        txt_Stage.GetComponent<Text>().text = "Stage:" + stageLevel.ToString();
        txt_Score.GetComponent<Text>().text = "Score:" + score.ToString();
    }

    internal void Hide() {
        gameObject.SetActive(false);
    }


    public void EasingIn_Tick(float dt) {
        if (!isPanelEasingIn) {
            return;
        }
        if (timer <= duration) {
            timer += dt;
            transform.localPosition = GFEasing.Ease2D(GFEasingEnum.OutElastic, timer, duration, startPos, endPos);
        } else {
            timer = 0;
            isPanelEasingIn = false;
        }
    }
}