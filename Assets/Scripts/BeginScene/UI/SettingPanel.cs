﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingPanel : BasePanel<SettingPanel>
{
    public Button btnBack;

    public Toggle togMusic;
    public Toggle togSound;

    public Slider sliderMusic;
    public Slider sliderSound;
    public Slider sliderFortSpeed;

    public PlayerObj player;
    // Start is called before the first frame update
    void Start()
    {
        sliderFortSpeed.value = player.headRoundSpeed;

        btnBack.onClick.AddListener(() =>
        {
            HideMe();
            if (SceneManager.GetActiveScene().name == "BeginScene")
            {
                BeginPanel.Instance.ShowMe();
            }

        });
        togMusic.onValueChanged.AddListener((v) =>
        {
            GameDataMgr.Instance.OpenOrCloseMusic(v);
        });
        togSound.onValueChanged.AddListener((v) =>
        {
            GameDataMgr.Instance.OpenOrCloseSound(v);
        });
        sliderMusic.onValueChanged.AddListener((value) =>
        {
            GameDataMgr.Instance.ChangeBKValue(value);
        });
        sliderSound.onValueChanged.AddListener((value) =>
        {
            GameDataMgr.Instance.ChangeSoundValue(value);
        });
        sliderFortSpeed.onValueChanged.AddListener((value) =>
        {
            player.headRoundSpeed = value;
        });

        HideMe();
    }

    public void UpdatePanelInfo()
    {
        MusicData data = GameDataMgr.Instance.musicData;
        sliderMusic.value = data.bkmusicValue;
        sliderSound.value = data.soundValue;

        togMusic.isOn = data.isBKMusicOpen;
        togSound.isOn = data.isSoundOpen;
    }
    public override void HideMe()
    {
        base.HideMe();
        Time.timeScale = 1;
    }
    public override void ShowMe()
    {
        base.ShowMe();

        UpdatePanelInfo();
    }
}
