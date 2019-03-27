using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[CreateAssetMenu(menuName = "CreateManagerVarsContainer")]
public class ManagerVars : ScriptableObject
{
    // 随机的背景图
    public List<Sprite> bgThemeSpriteList = new List<Sprite>();

    // 平台的预制体
    public GameObject normalPlatform;

    public static ManagerVars GetManagerVars()
    {
        return Resources.Load<ManagerVars>("ManagerVarsContainer");
    }

    public float nexXPos = 0.554f, nextYPos = 0.645f;
}
