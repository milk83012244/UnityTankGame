using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 開始面板
/// </summary>
public class BeginPanel : BasePanel<BeginPanel>
{
    public Button btnStart;
    public Button btnSetting;
    public Button btnQuit;
    public Button btnRank;

    //初始化控件
    private void Start()
    {
        //限制滑鼠視窗範圍
        Cursor.lockState = CursorLockMode.Confined;

        btnStart.onClick.AddListener(() =>
        {
            //切換遊戲場景
            SceneManager.LoadScene("GameScene");
        });

        btnSetting.onClick.AddListener(() =>
        {
            //顯示設定面板
            SettingPanel.Instance.ShowMe();
            HideMe();
        });

        btnRank.onClick.AddListener(() =>
        {
            //顯示排行榜面板
            RankPanel.Instance.ShowMe();
            HideMe();
        });

        btnQuit.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
