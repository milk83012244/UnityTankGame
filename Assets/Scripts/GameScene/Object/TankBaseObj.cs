using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 坦克基類
/// </summary>
public abstract class TankBaseObj : MonoBehaviour
{
    //坦克相關數值
    public int atk;
    public int def;
    public int maxHp;
    public int hp;
    //相關移動
    public float moveSpeed;
    public float roundSpeed;
    public float headRoundSpeed;
    //坦克砲台
    public Transform tankHead;
    //死亡特效
    public GameObject deadEff;
    //抽象開火方法
    public abstract void Fire();
    //受傷
    public virtual void Wound(TankBaseObj other)
    {
        //計算傷害
        int dmg = other.atk - this.def;
        if (dmg <= 0)
        {
            return;
        }
        //扣HP
        this.hp -= dmg;
        if (this.hp <= 0)
        {
            this.hp = 0;
            this.Dead();
        }
    }
    //死亡方法
    public virtual void Dead()
    {
        Destroy(this.gameObject);
        //生成特效
        if (deadEff!=null)
        {
            GameObject effObj = Instantiate(deadEff, this.transform.position, this.transform.rotation);
            AudioSource audioSource = effObj.GetComponent<AudioSource>();
            audioSource.volume = GameDataMgr.Instance.musicData.soundValue;
            audioSource.mute = !GameDataMgr.Instance.musicData.isSoundOpen;
            audioSource.Play();
            Destroy(effObj.gameObject, 1);
        }
    }
}
