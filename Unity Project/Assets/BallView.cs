using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Currently used basically as a tag.
// Not anymore ;)
public class BallView : MonoBehaviour
{
    public GameObject solidBall;

    private Coroutine timeOut;

    private Rigidbody2D _rigid;

    public Rigidbody2D Rigid { get => _rigid; }

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    public void Start()
    {
        solidBall.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        var paddle = col.gameObject.GetComponent<PaddleView>();
        if (paddle != null)
        {
            if (timeOut != null)
            {
                StopCoroutine(timeOut);
            }

            timeOut = StartCoroutine(TimeOutBall());

            GameController.instance.OnPaddleBallCollision(paddle, _rigid);
        }
        else if (col.gameObject.GetComponent<BallResetOnHit>() != null)
        {
            GameController.instance.ResetBall();
        }
    }

    private IEnumerator TimeOutBall()
    {
        solidBall.SetActive(true);

        yield return new WaitForSeconds(0.15f);

        solidBall.SetActive(false);
    }
}
