using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rand = UnityEngine.Random;

public enum ControllerType
{
    KeyboardLeft,
    KeyboardRight,
    Controller
}

[Serializable]
public struct Player
{
    public ControllerType controllerType;
    public PlayerView view;
}

public class GameController : MonoBehaviour
{
    public Player playerL;
    public Player playerR;

    public Rigidbody2D ball;

    public float paddleSpeed = 3.0f;
    public float paddleDrag = 20.0f;

    public Vector2 ballDirScale = new Vector2(0.5f, 1.0f);

    // Start is called before the first frame update
    void Start()
    {
        playerL.view.GetComponent<Rigidbody2D>().drag = paddleDrag;
        playerR.view.GetComponent<Rigidbody2D>().drag = paddleDrag;

        float randAngle = Rand.value * Mathf.PI;

        var ballDirection = (new Vector2(Mathf.Cos(randAngle), -Mathf.Sin(randAngle)) * ballDirScale).normalized;

        ball.velocity = ballDirection * 6.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FixedUpdatePlayer(playerL);
        FixedUpdatePlayer(playerR);
    }

    private void FixedUpdatePlayer(Player player)
    {
        var paddleView = player.view;

        float moveScale = 0.0f;

        switch (player.controllerType)
        {
            case ControllerType.KeyboardLeft:
                moveScale += (Input.GetKey(KeyCode.W)) ? 1 : 0;
                moveScale += (Input.GetKey(KeyCode.S)) ? -1 : 0;
                break;

            case ControllerType.KeyboardRight:
                moveScale += (Input.GetKey(KeyCode.I)) ? 1 : 0;
                moveScale += (Input.GetKey(KeyCode.K)) ? -1 : 0;
                break;

            case ControllerType.Controller:
                moveScale = Input.GetAxisRaw("Vertical");
                break;
        }

        paddleView.Move(paddleSpeed * moveScale);
    }
}
