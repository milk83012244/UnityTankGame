using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObj : TankBaseObj
{
    private Transform targetPos;

    public Transform[] randomPos;

    public Transform lookAtTarget;

    public Transform[] shootPos;

    public GameObject bulletObj;

    public HPBar hpBar;

    public float fireDis = 5;

    public float fireOffectTime;
    private float nowTime;

// Start is called before the first frame update
void Start()
    {
        RandomPos();

        hpBar.SetMaxHp(this.maxHp);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(targetPos);
        this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        if (Vector3.Distance(this.transform.position,targetPos.position)<0.5f)
        {
            RandomPos();
        }

        if (lookAtTarget != null)
        {
            tankHead.LookAt(lookAtTarget);

            if (Vector3.Distance(this.transform.position,lookAtTarget.position)<=fireDis)
            {
                nowTime += Time.deltaTime;
                if (nowTime >= fireOffectTime)
                {
                    Fire();
                    nowTime = 0;
                }
              
            }
        }
    }


    private void RandomPos()
    {
        if (randomPos.Length == 0)
        {
            return;
        }
        targetPos = randomPos[Random.Range(0, randomPos.Length)];
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
    public override void Dead()
    {
        base.Dead();

        GamePanel.Instance.AddScore(10);
    }
    public override void Wound(TankBaseObj other)
    {
        base.Wound(other);
        hpBar.SetHp(this.hp);
    }
}
