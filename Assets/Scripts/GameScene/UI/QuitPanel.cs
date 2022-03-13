using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuitPanel : BasePanel<QuitPanel>
{
    public Button btnSure;
    public Button btnBack;

    // Start is called before the first frame update
    void Start()
    {
        btnSure.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("BeginScene");
        });
        btnBack.onClick.AddListener(() =>
        {
            HideMe();
        });
        HideMe();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void HideMe()
    {
        base.HideMe();
        Time.timeScale = 1;
    }
}
