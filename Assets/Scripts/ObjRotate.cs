using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 自動旋轉
/// </summary>
public class ObjRotate : MonoBehaviour
{
    public float rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }
}
