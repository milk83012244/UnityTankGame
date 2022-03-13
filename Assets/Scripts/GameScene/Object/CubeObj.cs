using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeObj : MonoBehaviour
{
    public GameObject[] rewardObj;

    public GameObject getEff;
    private void OnTriggerEnter(Collider other)
    {

        int rangeInt = Random.Range(0, 100);
        if (rangeInt < 50)
        {
            rangeInt = Random.Range(0, rewardObj.Length);
            Instantiate(rewardObj[rangeInt], this.transform.position, this.transform.rotation);
        }
        GameObject eff = Instantiate(getEff, this.transform.position, this.transform.rotation);
        AudioSource audioSource = eff.GetComponent<AudioSource>();
        audioSource.volume = GameDataMgr.Instance.musicData.soundValue;
        audioSource.mute = !GameDataMgr.Instance.musicData.isSoundOpen;
        audioSource.Play();
        Destroy(eff.gameObject, 1f);

        Destroy(this.gameObject);
    }
}
