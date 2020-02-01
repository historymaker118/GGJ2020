using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Currently used basically as a tag.
// Not anymore ;)
public class BallView : MonoBehaviour
{
    public GameObject solidBall;

    private Coroutine timeOut;


    public void Start()
    {
        solidBall.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<PaddleView>() != null)
        {
            if (timeOut != null)
            {
                StopCoroutine(timeOut);
            }

            timeOut = StartCoroutine(TimeOutBall());
        }
    }

    private IEnumerator TimeOutBall()
    {
        solidBall.SetActive(true);

        yield return new WaitForSeconds(0.15f);

        solidBall.SetActive(false);
    }
}
