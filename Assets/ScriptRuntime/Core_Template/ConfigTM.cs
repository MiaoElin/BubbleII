using UnityEngine;

[CreateAssetMenu(menuName = "TM/ConfigTM", fileName = "ConfigTM")]
public class ConfigTM : ScriptableObject {
    public AudioClip bgm1;
    public AudioClip sfx_BubbleBroke;
    public AudioClip sfx_click;
    public AudioClip sfx_win;
    public Sprite[] vfx_BubbleBroke;
    public Sprite[] vfx_Win;
}