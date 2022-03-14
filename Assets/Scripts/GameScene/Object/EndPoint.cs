using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 終點
/// </summary>
public class EndPoint : MonoBehaviour
{
    //觸發檢測
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 0;
            WinPanel.Instance.ShowMe();
        }
    }
}
