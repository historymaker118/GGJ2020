using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleView : MonoBehaviour
{
    private Rigidbody2D _rigid;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(float speed)
    {
        _rigid.velocity += Vector2.up * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BallView>() != null)
        {
            GameController.instance.OnPaddleBallCollision(this, collision.rigidbody);
        }
    }
}
