using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 小地圖移動
/// </summary>
public class MapCameraMove : MonoBehaviour
{
    public Transform targetPlayer;
    public float H=10;

    Vector3 pos;
    // Update is called once per frame
    void LateUpdate()
    {
        //檢測有沒有玩家目標
        if (targetPlayer == null)
        {
            return;
        }
        //讓小地圖攝影機跟著玩家移動
        pos.x = targetPlayer.position.x;
        pos.z = targetPlayer.position.z;
        pos.y = H;
        this.transform.position = pos;
    }
}
