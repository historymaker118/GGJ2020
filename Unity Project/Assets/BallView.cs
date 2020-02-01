﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Currently used basically as a tag.
// Not anymore ;)
public class BallView : MonoBehaviour
{
    private Rigidbody2D _rigid;

    public Rigidbody2D Rigid { get => _rigid; }

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    public void Start()
    {

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        var paddle = col.gameObject.GetComponent<PaddleView>();
        if (paddle != null)
        {
            GameController.instance.OnPaddleBallCollision(paddle, _rigid);
        }
        else if (col.gameObject.GetComponent<BallResetOnHit>() != null)
        {
            GameController.instance.ResetBall();
        }
    }
}
