using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_PropType
{
    Atk,
    Def,
    MaxHp,
    Hp,
}
public class PropReward : MonoBehaviour
{
    public int changeValue;

    public E_PropType type = E_PropType.Atk;

    public GameObject getEff;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerObj player = other.GetComponent<PlayerObj>();
            switch (type)
            {
                case E_PropType.Atk:
                    player.atk += changeValue;
                    break;
                case E_PropType.Def:
                    player.def += changeValue;
                    break;
                case E_PropType.MaxHp:
                    player.maxHp += changeValue;
                    GamePanel.Instance.UpdateHP(player.hp, player.maxHp);
                    break;
                case E_PropType.Hp:
                    player.hp += changeValue;
                    if (player.hp >= player.maxHp)
                    {
                        player.hp = player.maxHp;
                    }
                    GamePanel.Instance.UpdateHP(player.hp, player.maxHp);
                    break;
            }
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
