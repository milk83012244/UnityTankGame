using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinPanel : BasePanel<WinPanel>
{
    public Button btnSure;
    public InputField inputInfo;

    private void Start()
    {
        Time.timeScale = 1;

        btnSure.onClick.AddListener(() =>
        {
            GameDataMgr.Instance.AddRankInfo(inputInfo.text, GamePanel.Instance.nowScore, GamePanel.Instance.nowTime);

            SceneManager.LoadScene("BeginScene");
        });
        HideMe();
    }
}
