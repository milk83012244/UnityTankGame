using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家物件
/// </summary>
public class PlayerObj : TankBaseObj
{
    //現在裝備砲台
    public WeaponObj nowWeapon;
    //放置砲台位置
    public Transform weaponPos;
    private void Update()
    {
        //Cursor.lockState = CursorLockMode.Confined;
        //前後移動
        this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime * Input.GetAxis("Vertical"));
        //左右旋轉
        this.transform.Rotate(Vector3.up * roundSpeed * Time.deltaTime * Input.GetAxis("Horizontal"));
        //砲台隨滑鼠x軸移動
        tankHead.transform.Rotate(Vector3.up * headRoundSpeed * Time.deltaTime * Input.GetAxis("Mouse X"));
        //點擊滑鼠左鍵開火
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }
    //開火
    public override void Fire()
    {
        //檢測有沒有裝備砲台
        if (nowWeapon != null)
        {
            nowWeapon.Fire();
        }
    }
    //死亡
    public override void Dead()
    {
        //base.Dead();
        //時間暫停 顯示失敗面板
        Time.timeScale = 0;
        LosePanel.Instance.ShowMe();
    }
    //受傷方法
    public override void Wound(TankBaseObj other)
    {
        base.Wound(other);
        //更新遊戲面板hp
        GamePanel.Instance.UpdateHP(this.hp, this.maxHp);
    }
    //更換砲台
    public void ChangeWeapon(GameObject weapon)
    {
        //檢測有沒有裝備砲台
        if (nowWeapon != null)
        {
            //刪除前一個砲台
            Destroy(nowWeapon.gameObject);
            nowWeapon = null;
        }
        //生成砲台放在砲台生成點
        GameObject weaponObj = Instantiate(weapon, weaponPos, false);
        nowWeapon = weaponObj.GetComponent<WeaponObj>();
        //設定子彈發射對象
        nowWeapon.SetFather(this);
    }
}
