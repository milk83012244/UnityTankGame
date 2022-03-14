using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 獎勵類型
/// </summary>
public enum E_PropType
{
    Atk,
    Def,
    MaxHp,
    Hp,
}
/// <summary>
/// 掉落獎勵
/// </summary>
public class PropReward : MonoBehaviour
{
    //設定道具數值
    public int changeValue;

    public E_PropType type = E_PropType.Atk;

    public GameObject getEff;
    //被碰到後觸發
    private void OnTriggerEnter(Collider other)
    {
        //如果是玩家碰到
        if (other.CompareTag("Player"))
        {
            PlayerObj player = other.GetComponent<PlayerObj>();
            switch (type)
            {
                //增加攻擊
                case E_PropType.Atk:
                    player.atk += changeValue;
                    break;
                //增加防禦
                case E_PropType.Def:
                    player.def += changeValue;
                    break;
                //增加最大HP
                case E_PropType.MaxHp:
                    player.maxHp += changeValue;
                    GamePanel.Instance.UpdateHP(player.hp, player.maxHp);
                    break;
                //回復HP
                case E_PropType.Hp:
                    player.hp += changeValue;
                    if (player.hp >= player.maxHp)
                    {
                        player.hp = player.maxHp;
                    }
                    GamePanel.Instance.UpdateHP(player.hp, player.maxHp);
                    break;
            }
            //生成特效
            GameObject effObj = Instantiate(getEff, this.transform.position, this.transform.rotation);
            AudioSource audioSource = effObj.GetComponent<AudioSource>();
            audioSource.volume = GameDataMgr.Instance.musicData.soundValue;
            audioSource.mute = !GameDataMgr.Instance.musicData.isSoundOpen;
            audioSource.Play();
            Destroy(effObj.gameObject, 1f);

            Destroy(this.gameObject);
        }
    }
}
