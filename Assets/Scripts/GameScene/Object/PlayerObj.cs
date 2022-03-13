using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObj : TankBaseObj
{
    public WeaponObj nowWeapon;

    public Transform weaponPos;
    private void Update()
    {
        //Cursor.lockState = CursorLockMode.Confined;

        this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime * Input.GetAxis("Vertical"));
        this.transform.Rotate(Vector3.up * roundSpeed * Time.deltaTime * Input.GetAxis("Horizontal"));
        tankHead.transform.Rotate(Vector3.up * headRoundSpeed * Time.deltaTime * Input.GetAxis("Mouse X"));
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }
    public override void Fire()
    {
        if (nowWeapon != null)
        {
            nowWeapon.Fire();
        }
    }
    public override void Dead()
    {
        //base.Dead();
        Time.timeScale = 0;
        LosePanel.Instance.ShowMe();
    }
    public override void Wound(TankBaseObj other)
    {
        base.Wound(other);
        GamePanel.Instance.UpdateHP(this.hp, this.maxHp);
    }
    public void ChangeWeapon(GameObject weapon)
    {
        if (nowWeapon != null)
        {
            Destroy(nowWeapon.gameObject);
            nowWeapon = null;
        }

        GameObject weaponObj = Instantiate(weapon, weaponPos, false);
        nowWeapon = weaponObj.GetComponent<WeaponObj>();
        nowWeapon.SetFather(this);
    }
}
