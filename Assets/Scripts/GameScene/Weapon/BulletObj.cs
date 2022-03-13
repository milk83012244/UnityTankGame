using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObj : MonoBehaviour
{
    public float moveSpeed = 50;

    public TankBaseObj fatherObj;

    public GameObject effObj;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject,5);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube")||
            other.CompareTag("Player") &&fatherObj.CompareTag("Enemy")||
            other.CompareTag("Enemy") && fatherObj.CompareTag("Player"))
        {
            TankBaseObj obj = other.GetComponent<TankBaseObj>();
            if (obj!=null)
            {
                obj.Wound(fatherObj);
            }

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

    public void SetFather(TankBaseObj obj)
    {
        fatherObj = obj;
    }

}
