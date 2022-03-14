using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 子彈物件腳本
/// </summary>
public class BulletObj : MonoBehaviour
{
    public float moveSpeed = 50;
    //發射子彈的對象
    public TankBaseObj fatherObj;
    //特效物件
    public GameObject effObj;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject,5);
    }

    // Update is called once per frame
    void Update()
    {
        //子彈移動
        this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
    //子彈觸發相關
    private void OnTriggerEnter(Collider other)
    {
        //檢測碰到的對象是方塊 還是玩家的子彈碰到敵人 還是敵人的子彈碰到玩家
        if (other.CompareTag("Cube")||
            other.CompareTag("Player") &&fatherObj.CompareTag("Enemy")||
            other.CompareTag("Enemy") && fatherObj.CompareTag("Player"))
        {
            //獲取碰到對象的坦克基類腳本
            TankBaseObj obj = other.GetComponent<TankBaseObj>();
            //檢測對象是否存在
            if (obj!=null)
            {
                obj.Wound(fatherObj);
            }
            //特效物件相關
            if (effObj != null)
            {
                effObj = Instantiate(effObj, this.transform.position, this.transform.rotation);
                AudioSource audioSource = effObj.GetComponent<AudioSource>();
                audioSource.volume = GameDataMgr.Instance.musicData.soundValue;
                audioSource.mute = !GameDataMgr.Instance.musicData.isSoundOpen;
                audioSource.Play();
                Destroy(effObj.gameObject,0.8f);
            }
            Destroy(this.gameObject);
        }
    }
    //設定發出子彈的對象
    public void SetFather(TankBaseObj obj)
    {
        fatherObj = obj;
    }

}
