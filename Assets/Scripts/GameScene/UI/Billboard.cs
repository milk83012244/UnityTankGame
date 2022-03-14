using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform cam;

    // Update is called once per frame
    void LateUpdate()
    {
        //讓攝影機看向血條的方向永遠相同
        transform.LookAt(transform.position + cam.forward);
    }
}
