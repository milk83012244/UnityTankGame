using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器物件
/// </summary>
public class WeaponObj : MonoBehaviour
{
    public GameObject bullet;

    public Transform[] shootPos;

    public TankBaseObj fatherObj;
    //設定發出子彈的物件
    public void SetFather(TankBaseObj obj)
    {
        fatherObj = obj;
    }
    //開火方法
    public void Fire()
    {
        //遍歷所有發射點
        for (int i = 0; i < shootPos.Length; i++)
        {
            //生成子彈
            GameObject obj = Instantiate(bullet, shootPos[i].position, shootPos[i].rotation);
            BulletObj bulletObj = obj.GetComponent<BulletObj>();
            bulletObj.SetFather(fatherObj);
        }
    }

}
