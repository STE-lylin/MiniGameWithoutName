using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] float speed = 2.0f;
    private RectTransform rect_bg;
    private RectTransform rect_bg1;
    private RectTransform rect_fg;
    private RectTransform rect_fg1;

    private GameObject settingPanels;
    // Start is called before the first frame update
    void Start()
    {
        rect_bg = GameObject.Find("background").GetComponent<RectTransform>();
        rect_bg1 = GameObject.Find("background1").GetComponent<RectTransform>();
        rect_fg = GameObject.Find("foreground").GetComponent<RectTransform>();
        rect_fg1 = GameObject.Find("foreground1").GetComponent<RectTransform>();

        settingPanels = GameObject.Find("SettingPanels");
        settingPanels.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //¿ØÖÆ±³¾°Í¼Æ¬µÄÒÆ¶¯
        rect_bg.anchoredPosition -= new Vector2(speed * Time.deltaTime, 0);
        if (rect_bg.anchoredPosition.x <= -1920)
        {
            rect_bg.anchoredPosition += new Vector2(3640, 0);
        }
        rect_bg1.anchoredPosition -= new Vector2(speed * Time.deltaTime, 0);
        if (rect_bg1.anchoredPosition.x <= -1920)
        {
            rect_bg1.anchoredPosition += new Vector2(3640, 0);
        }

        rect_fg.anchoredPosition += new Vector2(speed * Time.deltaTime, 0);
        if (rect_fg.anchoredPosition.x >= 1920)
        {
            rect_fg.anchoredPosition -= new Vector2(3640, 0);
        }
        rect_fg1.anchoredPosition += new Vector2(speed * Time.deltaTime, 0);
        if (rect_fg1.anchoredPosition.x >= 1920)
        {
            rect_fg1.anchoredPosition -= new Vector2(3640, 0);
        }
    }

    public void onClick_StartButton()
    {
        SceneManager.LoadScene("Scene");
    }

    public void onClick_SettingButton()
    {
        settingPanels.SetActive(true);
    }

    public void onClick_ExitButton()
    {
        Application.Quit();
    }
}
