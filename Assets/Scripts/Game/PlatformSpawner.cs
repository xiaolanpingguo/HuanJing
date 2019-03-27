using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public Vector3 startSpawnPos;
    private ManagerVars vars;
    private int SpawnPlatformNum = 5;
    private Vector3 platformSpawnPos;
    private bool IsLeftSpawn = false;

    // 开始生成
    void Start()
    {
        platformSpawnPos = startSpawnPos;
        vars = ManagerVars.GetManagerVars();
        for (int i = 0; i < 5; ++i)
        {
            SpawnPlatformNum = 5;
            DecidePath();
        }
    }

    // 确定路径
    private void DecidePath()
    {
        if (SpawnPlatformNum > 0)
        {
            SpawnPlatformNum--;
            SpawnPlatform();
        }
        else
        {
            IsLeftSpawn = !IsLeftSpawn;
            SpawnPlatformNum = Random.Range(1, 4);
            SpawnPlatform();
        }
    }

    // 生成平台
    private void SpawnPlatform()
    {
       GameObject go =Instantiate(vars.normalPlatform, transform);
        go.transform.position = platformSpawnPos;
        if (IsLeftSpawn)
        {
            platformSpawnPos = new Vector3(platformSpawnPos.x - vars.nexXPos,
                platformSpawnPos.y + vars.nextYPos);
        }
        else
        {
            platformSpawnPos = new Vector3(platformSpawnPos.x + vars.nexXPos,
                 platformSpawnPos.y + vars.nextYPos);
        }
    }
}
