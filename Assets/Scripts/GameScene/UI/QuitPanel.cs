using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 退出面板
/// </summary>
public class QuitPanel : BasePanel<QuitPanel>
{
    public Button btnSure;
    public Button btnBack;

    // Start is called before the first frame update
    void Start()
    {
        btnSure.onClick.AddListener(() =>
        {
            //切換到開始場景
            SceneManager.LoadScene("BeginScene");
        });
        btnBack.onClick.AddListener(() =>
        {
            HideMe();
        });
        HideMe();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //覆寫隱藏自己讓時間啟動
    public override void HideMe()
    {
        base.HideMe();
        Time.timeScale = 1;
    }
}
