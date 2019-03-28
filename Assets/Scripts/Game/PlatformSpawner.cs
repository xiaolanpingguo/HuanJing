using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlatformGroupType
{
    Grass,
    Winter
}

public class PlatformSpawner : MonoBehaviour
{
    private const float NORMAL_PLAT_START_X_POS = 0.0f;
    private const float NORMAL_PLAT_START_Y_POS = -2.4f;

    private const float PLAYER_START_X_POS = 0.0f;
    private const float PLAYER_START_Y_POS = -1.6f;

    public Vector3 startSpawnPos;
    private ManagerVars vars;
    private int SpawnPlatformNum = 5;
    private Vector3 platformSpawnPos;
    private bool IsLeftSpawn = false;

    // 选择的平台图
    private Sprite selectPlatformSprite;

    // 组合的平台的类型
    private PlatformGroupType groupType;

    // 钉子方向的平台是否在左边
    private bool spikeSpawnLeft = false;

    // 钉子方向平台的位置
    private Vector3 spikeDirPlatformPos;

    // 生成钉子平台之后需要在钉子方向生成的平台数量
    private int afterSpawnSpikeCount; // 
    private bool isSpawnSpike;

    void Awake()
    {
        vars = ManagerVars.GetManagerVars();
        EventCenter.AddListener(EventDefine.DecidePath, DecidePath);
    }

    void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.DecidePath, DecidePath);
    }

    // 开始生成
    void Start()
    {
        // 随机主题
        RandomPlatformTheme();

        startSpawnPos = new Vector3(NORMAL_PLAT_START_X_POS, NORMAL_PLAT_START_Y_POS, 0);
        platformSpawnPos = startSpawnPos;
        for (int i = 0; i < 5; ++i)
        {
            SpawnPlatformNum = 5;
            DecidePath();
        }

        // 生成人物
        GameObject go = Instantiate(vars.characterPre);
        go.transform.position = new Vector3(PLAYER_START_X_POS, PLAYER_START_Y_POS, 0);
    }

    // 确定路径
    private void DecidePath()
    {
        //if (isSpawnSpike)
        //{
        //    AfterSpawnSpike();
        //    return;
        //}

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

    // 生成平台并且计算位置
    private void SpawnPlatform()
    {
        int dir = Random.Range(0, 2);

        // 生成单个平台
        if (SpawnPlatformNum >= 1)
        {
            SpawnNormalPlatform(dir);
        }
        // 当生成的平台是最后时，生成组合平台
        else if (SpawnPlatformNum == 0)
        {
            int ran = Random.Range(0, 3);
           
            // 生成通用组合平台
            if (ran == 0)
            {
                SpawnCommonPlatform(dir);
            }
            // 生成主题组合平台
            else if (ran == 1)
            {
                switch (groupType)          
                {
                    case PlatformGroupType.Grass:
                        SpawnGrassPlatform(dir);
                        break;
                    case PlatformGroupType.Winter:
                        SpawnWinterPlatform(dir);
                        break;
                    default:
                        break;
                }
            }
            // 生成钉子组合平台
            else 
            {
                SpawnSpikePlatform(IsLeftSpawn ? 0 : 1);

                // 钉子在左边
                isSpawnSpike = true;
                afterSpawnSpikeCount = 4;
                if (spikeSpawnLeft)
                {
                    spikeDirPlatformPos = new Vector3(platformSpawnPos.x - 1.65f,
                        platformSpawnPos.y + vars.nextYPos, 0);
                }
                else
                {
                    spikeDirPlatformPos = new Vector3(platformSpawnPos.x + 1.65f,
                        platformSpawnPos.y + vars.nextYPos, 0);
                }
            }

            if (IsLeftSpawn)
            {
                platformSpawnPos = new Vector3(platformSpawnPos.x - vars.nexXPos,
                    platformSpawnPos.y + vars.nextYPos, 0);
            }
            else
            {
                platformSpawnPos = new Vector3(platformSpawnPos.x + vars.nexXPos,
                     platformSpawnPos.y + vars.nextYPos, 0);
            }
        }
    }

    // 生成普通平台
    private void SpawnNormalPlatform(int obstacleDir)
    {
        GameObject go = Instantiate(vars.normalPlatformPre, transform);
        go.transform.position = platformSpawnPos;
        go.GetComponent<PlatformScript>().Init(selectPlatformSprite, obstacleDir);
    }

    // 随机生成平台的主题
    private void RandomPlatformTheme()
    {
        int ran = Random.Range(0, vars.platformThemeSpriteList.Count);
        selectPlatformSprite = vars.platformThemeSpriteList[ran];

        if (ran == 2)
        {
            groupType = PlatformGroupType.Winter;
        }
        else
        {
            groupType = PlatformGroupType.Grass;
        }
    }

    // 生成通用组合平台
    private void SpawnCommonPlatform(int obstacleDir)
    {
        int ran = Random.Range(0, vars.commonPlatformGroup.Count);
        GameObject go = Instantiate(vars.commonPlatformGroup[ran], transform);
        go.transform.position = platformSpawnPos;
        go.GetComponent<PlatformScript>().Init(selectPlatformSprite, obstacleDir);
    }

    // 生成草地组合平台
    private void SpawnGrassPlatform(int obstacleDir)
    {
        int ran = Random.Range(0, vars.grassPlatformGroup.Count);
        GameObject go = Instantiate(vars.grassPlatformGroup[ran], transform);
        go.transform.position = platformSpawnPos;
        go.GetComponent<PlatformScript>().Init(selectPlatformSprite, obstacleDir);
    }

    // 生成冬季组合平台
    private void SpawnWinterPlatform(int obstacleDir)
    {
        int ran = Random.Range(0, vars.winnterPlatformGroup.Count);
        GameObject go = Instantiate(vars.winnterPlatformGroup[ran], transform);
        go.transform.position = platformSpawnPos;
        go.GetComponent<PlatformScript>().Init(selectPlatformSprite, obstacleDir);
    }

    // 生成钉子组合平台
    private void SpawnSpikePlatform(int dir)
    {
        GameObject go = null;

        // 左边
        if (dir == 0)
        {
            spikeSpawnLeft = true;
            go = Instantiate(vars.spikePlatformLeft, transform);
        }
        else
        {
            spikeSpawnLeft = false;
            go = Instantiate(vars.spikePlatformRight, transform);
        }

        go.transform.position = platformSpawnPos;
        go.GetComponent<PlatformScript>().Init(selectPlatformSprite, dir);
       // go.SetActive(true);
    }

    // 生成钉子平台之后，需要生成的平台，包括钉子方向，也包括原来的方向
    private void AfterSpawnSpike()
    {
        if (afterSpawnSpikeCount > 0)
        {
            afterSpawnSpikeCount--;
            for (int i = 0; i < 2; ++i)
            {
                // 生成原来方向的平台
                GameObject go = Instantiate(vars.normalPlatformPre, transform);
                if (i == 0)
                {
                    go.transform.position = platformSpawnPos;
                    if (spikeSpawnLeft)
                    {
                        platformSpawnPos = new Vector3(platformSpawnPos.x + vars.nexXPos,
                            platformSpawnPos.y + vars.nextYPos, 0);
                    }
                    else
                    {
                        platformSpawnPos = new Vector3(platformSpawnPos.x - vars.nexXPos,
                            platformSpawnPos.y + vars.nextYPos, 0);
                    }
                }
                else
                {
                    go.transform.position = spikeDirPlatformPos;
                    if (spikeSpawnLeft)
                    {
                        spikeDirPlatformPos = new Vector3(spikeDirPlatformPos.x - vars.nexXPos,
                            platformSpawnPos.y + vars.nextYPos, 0);
                    }
                    else
                    {
                        spikeDirPlatformPos = new Vector3(spikeDirPlatformPos.x + vars.nexXPos,
                            platformSpawnPos.y + vars.nextYPos, 0);
                    }
                }

                go.GetComponent<PlatformScript>().Init(selectPlatformSprite, 1);
            }
        }
        else
        {
            isSpawnSpike = false;
            DecidePath();
        }
    }
}
