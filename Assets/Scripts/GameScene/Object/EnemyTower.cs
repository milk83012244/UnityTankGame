using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵人防禦塔
/// </summary>
public class EnemyTower : TankBaseObj
{
    //開火間隔
    public float fireOffectTime = 1;
    //計時用
    private float nowTime = 0;
    //開火點
    public Transform[] shootPos;
    //子彈物件
    public GameObject bulletObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //開火間隔計時
        nowTime += Time.deltaTime;
        if (nowTime>=fireOffectTime)
        {
            Fire();
            nowTime = 0;
        }
    }
    //開火
    public override void Fire()
    {
        for (int i = 0; i < shootPos.Length; i++)
        {
            GameObject obj = Instantiate(bulletObj, shootPos[i].position, shootPos[i].rotation);
            BulletObj bullet = obj.GetComponent<BulletObj>();
            bullet.SetFather(this);
        }
    }
    //覆寫受傷方法置空就是 不會受傷 無敵狀態
    public override void Wound(TankBaseObj other)
    {
       
    }
}
