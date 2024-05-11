using System;
using UnityEngine.UI;
using UnityEngine;

public class Panel_Lose : MonoBehaviour {

    [SerializeField] Text title;
    [SerializeField] Text txt_Score;
    [SerializeField] Button btn_Restart;
    [SerializeField] Button btn_BackMenu;
    public Action OnRestartHandle;
    public Action OnBackMenuHandle;

    public Panel_Lose() {

    }

    public void Ctor() {

        btn_Restart.onClick.AddListener(() => {
            OnRestartHandle.Invoke();
        });

        btn_BackMenu.onClick.AddListener(() => {
            OnBackMenuHandle.Invoke();
        });

    }

    public void Init(int stageLevel, int score) {
        title.GetComponent<Text>().text = $"STAGE {stageLevel.ToString()} WIN";

        txt_Score.GetComponent<Text>().text = "SCORE:" + score.ToString();
    }
}