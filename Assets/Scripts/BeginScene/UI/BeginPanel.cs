using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BeginPanel : BasePanel<BeginPanel>
{
    public Button btnStart;
    public Button btnSetting;
    public Button btnQuit;
    public Button btnRank;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;

        btnStart.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("GameScene");
        });

        btnSetting.onClick.AddListener(() =>
        {
            SettingPanel.Instance.ShowMe();
            HideMe();
        });

        btnRank.onClick.AddListener(() =>
        {
            RankPanel.Instance.ShowMe();
            HideMe();
        });

        btnQuit.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
