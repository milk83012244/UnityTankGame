using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataMgr
{
    private static GameDataMgr instance = new GameDataMgr();

    public static GameDataMgr Instance
    {
        get
        {
            return instance;
        }
    }

    public MusicData musicData;

    public RankList rankData;
    private GameDataMgr()
    {
        musicData = PlayerPrefsDataMgr.Instance.LoadData(typeof(MusicData), "MusicData") as MusicData;

        if (musicData.notFirst)
        {
            musicData.notFirst = true;
            musicData.isBKMusicOpen = true;
            musicData.isSoundOpen = true;
            musicData.bkmusicValue = 1;
            musicData.soundValue = 1;
            PlayerPrefsDataMgr.Instance.SaveData(musicData, "MusicData");
        }

        rankData=PlayerPrefsDataMgr.Instance.LoadData(typeof(RankList), "RankList")as RankList;
    }

    public void OpenOrCloseMusic(bool isOpen)
    {
        musicData.isBKMusicOpen = isOpen;
        BKMusic.Instance.ChangeOpen(isOpen);
        PlayerPrefsDataMgr.Instance.SaveData(musicData, "MusicData");
    }
    public void OpenOrCloseSound(bool isOpen)
    {
        musicData.isSoundOpen = isOpen;

        PlayerPrefsDataMgr.Instance.SaveData(musicData, "MusicData");
    }

    public void ChangeBKValue(float value)
    {
        musicData.bkmusicValue = value;
        BKMusic.Instance.ChangeValue(value);
        PlayerPrefsDataMgr.Instance.SaveData(musicData, "MusicData");
    }

    public void ChangeSoundValue(float value)
    {
        musicData.soundValue = value;

        PlayerPrefsDataMgr.Instance.SaveData(musicData, "MusicData");
    }

    public void AddRankInfo(string name,int score, float time)
    {
        rankData.list.Add(new RankInfo(name, score, time));
        rankData.list.Sort((a, b) =>  a.time < b.time ? -1 : 1 );
        for (int i = rankData.list.Count -1; i >=10; i--)
        {
            rankData.list.RemoveAt(i);
        }
        PlayerPrefsDataMgr.Instance.SaveData(rankData, "RankInfo");
    }
}
