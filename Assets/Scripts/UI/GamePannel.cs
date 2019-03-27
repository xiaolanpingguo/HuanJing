using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePannel : MonoBehaviour
{
    private Button btn_Play;
    private Button btn_Pause;
    private Text txt_Score;
    private Text txt_DiamondNum;

    // Start is called before the first frame update
    void Start()
    {
        EventCenter.AddListener(EventDefine.ShowGamePannel, Show);
        Init();
    }

    private void Init()
    {
        btn_Play = transform.Find("btn_Play").GetComponent<Button>();
        btn_Play.gameObject.SetActive(false);
        btn_Play.onClick.AddListener(OnButtonPlay);

        btn_Pause = transform.Find("btn_Pause").GetComponent<Button>();
        btn_Pause.onClick.AddListener(OnButtonPause);

        txt_Score = transform.Find("txt_Score").GetComponent<Text>();
        //btn_Rank.onClick.AddListener(OnRankButtonClick);

        txt_DiamondNum = transform.Find("Diamond/txt_DiamondNum").GetComponent<Text>();
        //btn_Sound.onClick.AddListener(OnSoundButtonClick);

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowGamePannel, Show);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    // 开始
    private void OnButtonPlay()
    {
        btn_Play.gameObject.SetActive(false);
        btn_Pause.gameObject.SetActive(true);
    }

    // 暂停
    private void OnButtonPause()
    {
        btn_Play.gameObject.SetActive(true);
        btn_Pause.gameObject.SetActive(false);
    }
}
