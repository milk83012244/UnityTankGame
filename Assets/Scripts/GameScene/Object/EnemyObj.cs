using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵人物件
/// </summary>
public class EnemyObj : TankBaseObj
{
    //目標位置
    private Transform targetPos;
    //隨機移動位置
    public Transform[] randomPos;
    //看向目標
    public Transform lookAtTarget;
    //發射點
    public Transform[] shootPos;
    //子彈物件
    public GameObject bulletObj;

    public HPBar hpBar;
    //朝目標開火範圍
    public float fireDis = 5;
    //開火間隔
    public float fireOffectTime;
    //計時用變數
    private float nowTime;

// Start is called before the first frame update
void Start()
    {
        RandomPos();
        //設定最大血量
        hpBar.SetMaxHp(this.maxHp);
    }

    // Update is called once per frame
    void Update()
    {
        //看向目標
        this.transform.LookAt(targetPos);
        //移動
        this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        //檢測移動到隨機位置的距離接近0.5就換下一個
        if (Vector3.Distance(this.transform.position,targetPos.position)<0.5f)
        {
            RandomPos();
        }
        //檢測目標
        if (lookAtTarget != null)
        {
            //讓坦克頭部轉向目標
            tankHead.LookAt(lookAtTarget);
            //檢測與目標距離
            if (Vector3.Distance(this.transform.position,lookAtTarget.position)<=fireDis)
            {
                //開火間隔計時
                nowTime += Time.deltaTime;
                if (nowTime >= fireOffectTime)
                {
                    Fire();
                    nowTime = 0;
                }
              
            }
        }
    }

    //隨機位置移動
    private void RandomPos()
    {
        //如果沒有位置點就返回
        if (randomPos.Length == 0)
        {
            return;
        }
        //隨機位置
        targetPos = randomPos[Random.Range(0, randomPos.Length)];
    }
    //開火
    public override void Fire()
    {
        //遍歷開火點
        for (int i = 0; i < shootPos.Length; i++)
        {
            //生成子彈
            GameObject obj = Instantiate(bulletObj, shootPos[i].position, shootPos[i].rotation);
            BulletObj bullet = obj.GetComponent<BulletObj>();
            bullet.SetFather(this);
        }
    }
    //死亡
    public override void Dead()
    {
        base.Dead();
        //敵人死亡後加分
        GamePanel.Instance.AddScore(10);
    }
    //受傷
    public override void Wound(TankBaseObj other)
    {
        base.Wound(other);
        //把數值傳到血條
        hpBar.SetHp(this.hp);
    }
}
