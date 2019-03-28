using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    // 是否向左移动
    private bool isMoveLeft = false;

    // 是否正在跳跃
    private bool isJumping = false;

    private Vector3 nextPlatformLeft;
    private Vector3 nextPlatformRight;

    private ManagerVars vars;

    // Start is called before the first frame update
    void Start()
    {
        vars = ManagerVars.GetManagerVars();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!GameManager.Instance.IsGameStartd || GameManager.Instance.IsGameOver)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) && !isJumping)
        {
            EventCenter.Broadcast(EventDefine.DecidePath);
            Vector3 monsePos = Input.mousePosition;

            // 点击的是左边屏幕
            if (monsePos.x <= Screen.width / 2)
            {
                isMoveLeft = true;
            }
            else if (monsePos.x > Screen.width / 2)
            {
                isMoveLeft = false;
            }

            Jump();
            isJumping = true;
        }
    }

    private void Jump()
    {
        if (isMoveLeft)
        {
            transform.localScale = new Vector3(-1, 1, 1);

            // 移动0.2秒, 分开移动x,y是要制造一种重力效果
            transform.DOMoveX(nextPlatformLeft.x, 0.2f);
            transform.DOMoveY(nextPlatformLeft.y + 0.8f, 0.15f);
        }
        else
        {
            transform.DOMoveX(nextPlatformRight.x, 0.2f);
            transform.DOMoveY(nextPlatformRight.y + 0.8f, 0.15f);
            transform.localScale = Vector3.one;
        }
    }

    private void OnTriggerEnter2D(Collider2D collsion)
    {
        if (collsion.tag == "Platfrom")
        {
            isJumping = false; 
            Vector3 currentPlatformPos = collsion.gameObject.transform.position;
            nextPlatformLeft = new Vector3(currentPlatformPos.x - vars.nexXPos, currentPlatformPos.y + vars.nextYPos, 0);
            nextPlatformRight = new Vector3(currentPlatformPos.x + vars.nexXPos, currentPlatformPos.y + vars.nextYPos, 0);
        }
    }
}
