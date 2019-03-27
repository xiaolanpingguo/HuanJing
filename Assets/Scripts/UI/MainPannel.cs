using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPannel : MonoBehaviour
{
    private Button btn_Start;
    private Button btn_Shop;
    private Button btn_Rank;
    private Button btn_Sound;

    // Start is called before the first frame update
    void Start()
    {
        Init();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Init()
    {
        btn_Start = transform.Find("btn_Start").GetComponent<Button>();
        btn_Start.onClick.AddListener(OnStartButtonClick);

        btn_Shop = transform.Find("Btns/btn_Shop").GetComponent<Button>();
        btn_Shop.onClick.AddListener(OnShopButtonClick);

        btn_Rank = transform.Find("Btns/btn_Rank").GetComponent<Button>();
        btn_Rank.onClick.AddListener(OnRankButtonClick);

        btn_Sound = transform.Find("Btns/btn_Sound").GetComponent<Button>();
        btn_Sound.onClick.AddListener(OnSoundButtonClick);
    }

    // 开始按钮点击调用
    private void OnStartButtonClick()
    {
        EventCenter.Broadcast(EventDefine.ShowGamePannel);
        gameObject.SetActive(false);
    }

    // 商城按钮点击调用
    private void OnShopButtonClick()
    {

    }

    // 排行榜按钮点击调用
    private void OnRankButtonClick()
    {

    }

    // 声音按钮点击调用
    private void OnSoundButtonClick()
    {

    }
}
