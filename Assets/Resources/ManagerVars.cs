using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[CreateAssetMenu(menuName = "CreateManagerVarsContainer")]
public class ManagerVars : ScriptableObject
{
    // 随机的背景图
    public List<Sprite> bgThemeSpriteList = new List<Sprite>();

    // 平台的预制体
    public GameObject normalPlatformPre;
    public List<GameObject> commonPlatformGroup = new List<GameObject>();   // 通用平台
    public List<GameObject> grassPlatformGroup = new List<GameObject>();   // 草地平台
    public List<GameObject> winnterPlatformGroup = new List<GameObject>();   // 冬季平台
    public GameObject spikePlatformLeft;    // 左边的钉子平台
    public GameObject spikePlatformRight;    // 右边的钉子平台

    // 人物的预制体
    public GameObject characterPre;

    // 随机的平台主题
    public List<Sprite> platformThemeSpriteList = new List<Sprite>();


    public static ManagerVars GetManagerVars()
    {
        return Resources.Load<ManagerVars>("ManagerVarsContainer");
    }

    public float nexXPos = 0.554f, nextYPos = 0.645f;
}
