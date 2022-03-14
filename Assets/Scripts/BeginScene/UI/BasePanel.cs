using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 面板基類
/// </summary>
/// <typeparam name="T">面板類型</typeparam>
public abstract class BasePanel<T> : MonoBehaviour where T:class
{
    private static T instance;
    public static T Instance
    {
        get
        {
            return instance;
        }
    }
    //初始化Singleton
    private void Awake()
    {
        instance = this as T;
    }
    //顯示面板
    public virtual void ShowMe()
    {
        this.gameObject.SetActive(true);
    }
    //隱藏面板
    public virtual void HideMe()
    {
        this.gameObject.SetActive(false);
    }
}
