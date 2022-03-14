using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 背景音樂控制 Singleton
/// </summary>
public class BKMusic : MonoBehaviour
{
    private static BKMusic instance;

    public static BKMusic Instance => instance;

    private AudioSource audioSource;
    // Start is called before the first frame update
    //初始化數值
    void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
        ChangeOpen(GameDataMgr.Instance.musicData.isBKMusicOpen);
        ChangeValue(GameDataMgr.Instance.musicData.bkmusicValue);
    }
    //改變音量
    public void ChangeValue(float value)
    {
        audioSource.volume = value;
    }
    //音樂開關
    public void ChangeOpen(bool isOpen)
    {
        audioSource.mute = !isOpen;
    }
}
