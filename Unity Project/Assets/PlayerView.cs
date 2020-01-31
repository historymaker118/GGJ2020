using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private Rigidbody2D _rigid;

    private readonly float speed = 2.0f;

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

    public void MoveUp()
    {
        _rigid.velocity += Vector2.up * speed;
    }

    public void MoveDown()
    {
        _rigid.velocity += Vector2.up * -speed;
    }
}
