using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 武器獎勵
/// </summary>
public class WeaponReward : MonoBehaviour
{
    //砲台物件
    public GameObject[] weaponObj;
    //獲得特效
    public GameObject getEff;
    //檢測觸發器
    private void OnTriggerEnter(Collider other)
    {
        //檢測玩家對象觸發
        if (other.CompareTag("Player"))
        {
            //隨機砲台類型
            int index = Random.Range(0, weaponObj.Length);
            //獲得玩家腳本 調用切換武器方法
            PlayerObj player = other.GetComponent<PlayerObj>();
            player.ChangeWeapon(weaponObj[index]);
            //觸發特效
            GameObject eff = Instantiate(getEff, this.transform.position, this.transform.rotation);
            AudioSource audioSource = eff.GetComponent<AudioSource>();
            audioSource.volume = GameDataMgr.Instance.musicData.soundValue;
            audioSource.mute = !GameDataMgr.Instance.musicData.isSoundOpen;
            audioSource.Play();
            Destroy(eff.gameObject,1f);


            Destroy(this.gameObject);
        }
    }
}
