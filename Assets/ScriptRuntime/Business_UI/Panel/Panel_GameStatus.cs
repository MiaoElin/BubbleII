using System;
using UnityEngine;
using UnityEngine.UI;

public class Panel_GameStatus : MonoBehaviour {
    [SerializeField] Button btn_Change;
    [SerializeField] Text stageLevel_Txt;
    [SerializeField] Text score_txt;
    public Action OnChangeCickHandle;
    public void Ctor() {
        btn_Change.onClick.AddListener(() => {
            OnChangeCickHandle.Invoke();
        });
    }
}