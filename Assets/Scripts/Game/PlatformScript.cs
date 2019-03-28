using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    public SpriteRenderer[] spriteRenderers;
    public GameObject obstacle;

    public void Init(Sprite sprite, int dir)
    {
        for (int i = 0; i < spriteRenderers.Length; ++i)
        {
            spriteRenderers[i].sprite = sprite;
        }

        // 朝左边
        if (dir == 0)
        {
            if (obstacle != null)
            {
                obstacle.transform.localPosition = new Vector3(-obstacle.transform.localPosition.x, obstacle.transform.localPosition.y, 0);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
