using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
public class SoundCore {
    public AudioSource prefab;
    public AudioSource bgmPlayer;
    public AudioSource bubbleBreak;
    public AudioSource bubbleShoot;
    public AudioSource btnClick;
    public AudioSource winPlayer;

    public AsyncOperationHandle prefabHandle;


    public void LoadAll() {
        var hanle = Addressables.LoadAssetAsync<GameObject>("AudioSource");
        prefab = hanle.WaitForCompletion().GetComponent<AudioSource>();
        prefabHandle = hanle;

        GameObject sfx = new GameObject("SFX");
        bgmPlayer = GameObject.Instantiate(prefab, sfx.transform);
        bubbleBreak = GameObject.Instantiate(prefab, sfx.transform);
        bubbleShoot = GameObject.Instantiate(prefab, sfx.transform);
        btnClick = GameObject.Instantiate(prefab, sfx.transform);
        winPlayer = GameObject.Instantiate(prefab, sfx.transform);
    }

    public void Unload() {
        if (prefabHandle.IsValid()) {
            Addressables.Release(prefabHandle);
        }
    }

    public void BgmPlay(AudioClip clip) {
        bgmPlayer.loop = true;
        if (!bgmPlayer.isPlaying) {
            bgmPlayer.clip = clip;
            bgmPlayer.Play();
        }
    }

    public void BubbleShootPlay(AudioClip clip) {
        if (!bubbleShoot.isPlaying) {
            bubbleShoot.clip = clip;
            bubbleShoot.Play();
        }
    }

    public void BtnClick(AudioClip clip) {
        if (!btnClick.isPlaying) {
            btnClick.clip = clip;
            btnClick.Play();
        }
    }

    public void WinPlay(AudioClip clip) {
        if (!winPlayer.isPlaying) {
            winPlayer.clip = clip;
            winPlayer.Play();
        }
    }
}