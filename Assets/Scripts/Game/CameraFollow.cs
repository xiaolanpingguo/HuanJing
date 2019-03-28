﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    private Vector3 offset;
    private Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null && GameObject.FindGameObjectWithTag("Player") != null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            offset = target.position - transform.position;
        }
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            // 平滑移动
            float x = Mathf.SmoothDamp(transform.position.x, target.position.x - offset.x, ref velocity.x, 0.05f);
            float y = Mathf.SmoothDamp(transform.position.y, target.position.y - offset.y, ref velocity.y, 0.05f);

            // 防止相机抖动的太厉害
            if (y > transform.position.y)
            {
                transform.position = new Vector3(x, y, transform.position.z);
            }
        }
    }
}
