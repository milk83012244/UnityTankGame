using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePanel : BasePanel<GamePanel>
{
    public Button btnBack;
    public Button btnSetting;

    public Text txtScore;
    public Text txtTime;

    public Image imgHp;

    [HideInInspector]
    public int nowScore = 0;
    [HideInInspector]
    public float nowTime;

    private int hpW = 278;

    private int time;
    // Start is called before the first frame update
    void Start()
    {
        btnBack.onClick.AddListener(() =>
        {
            QuitPanel.Instance.ShowMe();
            Time.timeScale = 0;
        });
        btnSetting.onClick.AddListener(() =>
        {
            SettingPanel.Instance.ShowMe();
            Time.timeScale = 0;
        });
    }
    private void Update()
    {
        nowTime += Time.deltaTime;

        time = (int)nowTime;
        txtTime.text = "";
        if (time / 3600 > 0) 
        {
            txtTime.text += time / 3600 + "時";
        }
        if (time % 3600 / 60 > 0 || txtTime.text != "") 
        {
            txtTime.text += time % 3600 / 60 + "分";
        }
        txtTime.text += time % 60 + "秒";
    }
    public void AddScore(int score)
    {
        nowScore += score;

        txtScore.text = nowScore.ToString();
    }
    public void UpdateHP(int hp,int maxHp)
    {
        //更新血條的長度 Vector2(血量/最大血量*血條長度,血條寬度)
        (imgHp.transform as RectTransform).sizeDelta = new Vector2((float)hp / maxHp * hpW, 27);
    }
}
