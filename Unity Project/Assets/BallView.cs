﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Currently used basically as a tag.
// Not anymore ;)
public class BallView : MonoBehaviour
{
    public GameObject solidBall;


    public void Start()
    {
        solidBall.SetActive(false);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<PaddleView>() != null)
        {
            StartCoroutine(TimeOutBall());
        }
    }

    private IEnumerator TimeOutBall()
    {
        solidBall.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        solidBall.SetActive(false);
    }
}
