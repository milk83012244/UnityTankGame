using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

/// <summary>
/// PlayerPrefs數據管理類 統一管理數據的存檔讀取
/// </summary>
public class PlayerPrefsDataMgr
{
    private static PlayerPrefsDataMgr instance = new PlayerPrefsDataMgr();

    public static PlayerPrefsDataMgr Instance
    {
        get
        {
            return instance;
        }
    }

    private PlayerPrefsDataMgr()
    {

    }

    /// <summary>
    /// 存檔
    /// </summary>
    /// <param name="data">數據對象</param>
    /// <param name="keyName">數據的唯一名稱key 自己控制</param>
    public void SaveData( object data, string keyName )
    {
        //通過 Type 得到傳入數據對象的所有變數類型
        //結合 PlayerPrefs進行儲存

        #region 第一步 獲取傳入數據對象的所有變數類型
        Type dataType = data.GetType();
        //得到所有的變數類型
        FieldInfo[] infos = dataType.GetFields();
        #endregion

        #region 第二步 自己定義一個key的規則 進行數據儲存
        //儲存都是通過PlayerPrefs進行
        //保證key的唯一性 自訂一個key的規則

        //自訂一個規則
        // keyName_數據類型_變數類型_變數名
        #endregion

        #region 第三步 遍歷這些變數類型 進行數據儲存
        string saveKeyName = "";
        FieldInfo info;
        for (int i = 0; i < infos.Length; i++)
        {
            //對每一個變數類型 進行數據儲存
            //得到具體變數類型訊息
            info = infos[i];
            //通過FieldInfo可以直接獲取到 變數類型 和變數類型的名字
            //變數類型 info.FieldType.Name
            //變數類型名字 info.Name;

            //要根據自訂的key的拚接規則 來進行key的生成
            //Player1_PlayerInfo_Int32_age
            saveKeyName = keyName + "_" + dataType.Name + 
                "_" + info.FieldType.Name + "_" + info.Name;

            //得到了Key 按照規則
            //接下來就要来通過PlayerPrefs來進行儲存
            //如何獲取值
            //info.GetValue(data)
            //封装了一个方法 专门来存储值 
            SaveValue(info.GetValue(data), saveKeyName);
        }

        PlayerPrefs.Save();
        #endregion
    }

    private void SaveValue(object value, string keyName)
    {
        //通過PlayerPrefs進行儲存
        //根據數據類型不同 決定使用哪個API儲存
        //PlayerPrefs只支援3種類型儲存
        //判斷是甚麼類型調用方法儲存
        Type fieldType = value.GetType();

        //類型判斷
        //是不是int
        if( fieldType == typeof(int) )
        {
            Debug.Log("儲存int" + keyName);
            PlayerPrefs.SetInt(keyName, (int)value);
        }
        else if (fieldType == typeof(float))
        {
            Debug.Log("儲存float" + keyName);
            PlayerPrefs.SetFloat(keyName, (float)value);
        }
        else if (fieldType == typeof(string))
        {
            Debug.Log("儲存string" + keyName);
            PlayerPrefs.SetString(keyName, value.ToString());
        }
        else if (fieldType == typeof(bool))
        {
            Debug.Log("儲存bool" + keyName);
            //自己寫一個儲存bool的規則
            PlayerPrefs.SetInt(keyName, (bool)value ? 1 : 0);
        }
        //通過反射判斷父子關係
        //相當於判斷變數類型是不是IList 通過List的父類來判斷
        else if ( typeof(IList).IsAssignableFrom(fieldType) )
        {
            Debug.Log("儲存List" + keyName);
            //父類裝子類
            IList list = value as IList;
            //先儲存數量 
            PlayerPrefs.SetInt(keyName, list.Count);
            int index = 0;
            foreach (object obj in list)
            {
                //儲存具體的值
                SaveValue(obj, keyName + index);
                ++index;
            }
        }
        //判斷是不是Dictionary類型 通過Dictionary的父類來判斷
        else if( typeof(IDictionary).IsAssignableFrom(fieldType) )
        {
            Debug.Log("儲存Dictionary" + keyName);
            //父類裝子類
            IDictionary dic = value as IDictionary;
            //先存字典長度
            PlayerPrefs.SetInt(keyName, dic.Count);
            //遍歷儲存在Dictionary的具體值
            //區分Key
            int index = 0;
            foreach (object key in dic.Keys)
            {
                SaveValue(key, keyName + "_key_" + index);
                SaveValue(dic[key], keyName + "_value_" + index);
                ++index;
            }
        }
        //如果都不是這類型 可能是自訂類型
        else
        {
            SaveData(value, keyName);
        }
    }

    /// <summary>
    /// 讀取數據
    /// </summary>
    /// <param name="type">讀取想要的數據類型Type</param>
    /// <param name="keyName">數據的唯一名稱key 自己控制</param>
    /// <returns></returns>
    public object LoadData( Type type, string keyName )
    {
        //不用object對象傳入 而使用Type傳入
        //主要目的是節約一行代碼（在外部）
        //假設要讀取一個Player類型的數據 如果是object 就要在外部new一個對象傳入
        //如果是Type 只需要傳入一個Type typeof(Player) 然後在內部動態創建一個對象返回出來
        //這樣就可以在外部少寫一行代碼

        //根據傳入類型和數據名稱keyName
        //依照存檔時key的拼接規則 来进行数据的获取赋值 返回出去

        //根據傳入的Type創建一個對象來儲存數據
        object data = Activator.CreateInstance(type);
        //往這個new出來的對象儲存數據
        //得到所有變數
        FieldInfo[] infos = type.GetFields();
        //用於拼接key的字符串
        string loadKeyName = "";
        //用於儲存單個變數訊息的對象
        FieldInfo info;
        for (int i = 0; i < infos.Length; i++)
        {
            info = infos[i];
            //key的拼接規則一定要和儲存時一模一樣才能找到對應數據
            loadKeyName = keyName + "_" + type.Name +
                "_" + info.FieldType.Name + "_" + info.Name;

            //有key就可以結合PlayerPrefs來讀取
            //把數據裝到data中 
            info.SetValue(data, LoadValue(info.FieldType, loadKeyName));
        }
        return data;
    }

    /// <summary>
    /// 得到單個數據的方法
    /// </summary>
    /// <param name="fieldType">變數類型來判斷用哪個API來讀取</param>
    /// <param name="keyName">獲取具體數據</param>
    /// <returns></returns>
    private object LoadValue(Type fieldType, string keyName)
    {
        //根據變數類型來判斷用哪個API讀取
        if ( fieldType == typeof(int) )
        {
            return PlayerPrefs.GetInt(keyName, 0);
        }
        else if (fieldType == typeof(float))
        {
            return PlayerPrefs.GetFloat(keyName, 0);
        }
        else if (fieldType == typeof(string))
        {
            return PlayerPrefs.GetString(keyName, "");
        }
        else if (fieldType == typeof(bool))
        {
            //根據自訂儲存bool的規則來讀取值
            return PlayerPrefs.GetInt(keyName, 0) == 1 ? true : false;
        }
        else if( typeof(IList).IsAssignableFrom(fieldType) )
        {
            //得到長度
            int count = PlayerPrefs.GetInt(keyName, 0);
            //實例化一個List對象來賦值
            //用反射中的Activator進行快速實例化List對象
            IList list = Activator.CreateInstance(fieldType) as IList;
            for (int i = 0; i < count; i++)
            {
                //得到List中泛型的類型
                list.Add(LoadValue(fieldType.GetGenericArguments()[0], keyName + i));
            }
            return list;
        }
        else if( typeof(IDictionary).IsAssignableFrom(fieldType) )
        {
            //得到Dictionary的長度
            int count = PlayerPrefs.GetInt(keyName, 0);
            //實例化一個Dictionary對象 利用父類裝子類
            IDictionary dic = Activator.CreateInstance(fieldType) as IDictionary;
            Type[] kvType = fieldType.GetGenericArguments();
            for (int i = 0; i < count; i++)
            {
                dic.Add(LoadValue(kvType[0], keyName + "_key_" + i),
                         LoadValue(kvType[1], keyName + "_value_" + i));
            }
            return dic;
        }
        else
        {
            return LoadData(fieldType, keyName);
        }


        return null;
    }
}
