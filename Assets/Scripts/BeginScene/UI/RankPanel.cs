using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 排行榜面板
/// </summary>
public class RankPanel : BasePanel<RankPanel>
{

    public Button btnClose;

   // private List<Text> txtRank = new List<Text>();
    private List<Text> txtName = new List<Text>();
    private List<Text> txtScore = new List<Text>();
    private List<Text> txtTime = new List<Text>();
    // Start is called before the first frame update
    void Start()
    {
        btnClose.onClick.AddListener(() =>
        {
            HideMe();
            BeginPanel.Instance.ShowMe();
        });
        //遍歷排行榜的文字腳本 添加到List中
        for (int i = 1; i <= 10; i++)
        {
            //txtRank.Add(this.transform.Find("Rank/txtRank" + i).GetComponent<Text>());
            txtName.Add(this.transform.Find("Name/txtName" + i).GetComponent<Text>());
            txtScore.Add(this.transform.Find("Score/txtScore" + i).GetComponent<Text>());
            txtTime.Add(this.transform.Find("Time/txtTime" + i).GetComponent<Text>());
        }
        HideMe();
    }
    public override void ShowMe()
    {
        base.ShowMe();
        UpdatePanalInfo();
    }
    //更新排行榜面板數值
    public void UpdatePanalInfo()
    {
        //取得排行榜數據
        List<RankInfo> list = GameDataMgr.Instance.rankData.list;
        //遍歷添加數值到List裝的控件上
        for (int i = 0; i < list.Count; i++)
        {
            txtName[i].text = list[i].name;
            txtScore[i].text = list[i].score.ToString();
            //計時的文字
            int time = (int)list[i].time;
            txtTime[i].text = "";
            if (time / 3600 > 0)
            {
                txtTime[i].text += time / 3600 + "時";
            }
            if (time % 3600 / 60 > 0 || txtTime[i].text != "")
            {
                txtTime[i].text += time % 3600 / 60 + "分";
            }
            txtTime[i].text += time % 60 + "秒";
        }
    }
}
