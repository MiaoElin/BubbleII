using System;
using UnityEngine.UI;
using UnityEngine;

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

    public Panel_Win() {

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
}