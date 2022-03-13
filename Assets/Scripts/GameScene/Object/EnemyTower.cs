using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTower : TankBaseObj
{
    public float fireOffectTime = 1;
    private float nowTime = 0;

    public Transform[] shootPos;

    public GameObject bulletObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        nowTime += Time.deltaTime;
        if (nowTime>=fireOffectTime)
        {
            Fire();
            nowTime = 0;
        }
    }

    public override void Fire()
    {
        for (int i = 0; i < shootPos.Length; i++)
        {
            GameObject obj = Instantiate(bulletObj, shootPos[i].position, shootPos[i].rotation);
            BulletObj bullet = obj.GetComponent<BulletObj>();
            bullet.SetFather(this);
        }
    }
    public override void Wound(TankBaseObj other)
    {
       
    }
}
