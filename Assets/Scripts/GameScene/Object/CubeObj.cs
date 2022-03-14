using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 可破壞立方體物件
/// </summary>
public class CubeObj : MonoBehaviour
{
    //獎勵物件
    public GameObject[] rewardObj;
    //特效
    public GameObject getEff;
    //觸發相關
    private void OnTriggerEnter(Collider other)
    {
        //給一個隨機值
        int rangeInt = Random.Range(0, 100);
        //照隨機值生成獎勵
        if (rangeInt < 50)
        {
            //生成隨機獎勵
            rangeInt = Random.Range(0, rewardObj.Length);
            Instantiate(rewardObj[rangeInt], this.transform.position, this.transform.rotation);
        }
        //生成特效
        GameObject eff = Instantiate(getEff, this.transform.position, this.transform.rotation);
        AudioSource audioSource = eff.GetComponent<AudioSource>();
        audioSource.volume = GameDataMgr.Instance.musicData.soundValue;
        audioSource.mute = !GameDataMgr.Instance.musicData.isSoundOpen;
        audioSource.Play();
        Destroy(eff.gameObject, 1f);

        Destroy(this.gameObject);
    }
}
