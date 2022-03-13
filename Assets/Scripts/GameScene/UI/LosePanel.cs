using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LosePanel : BasePanel<LosePanel>
{
    public Button btnRestart;
    public Button btnBack;

    // Start is called before the first frame update
    void Start()
    {
        btnRestart.onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("GameScene");
        });
        btnBack.onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("BeginScene");
        });
        HideMe();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
