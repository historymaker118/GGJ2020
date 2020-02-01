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
    public PaddleView view;
}

public struct Bounds
{
    float min;
    float max;
}

public struct Environment
{
    BoxCollider2D topWall;
    BoxCollider2D bottomWall;
    BoxCollider2D leftWall;
    BoxCollider2D rightWall;
}

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public float MinPaddleDistance = 6.0f;
    public float MaxPaddleDistance = 40.0f;

    public float MinCameraScale = 5.0f;
    public float MaxCameraScale = 25.0f;

    public Player playerL;
    public Player playerR;

    public Rigidbody2D ball;

    public Environment env;

    new public Camera camera;

    public float paddleSpeed = 3.0f;
    public float paddleDrag = 20.0f;

    public float ballSpeed = 12.0f;

    public Vector2 ballDirScale = new Vector2(1.0f, 0.2f);

    [Range(0.0f, 1.0f)]
    public float paddleDistance = 0.05f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogAssertion("Cannot instantiate more than one GameController instance");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerL.view.GetComponent<Rigidbody2D>().drag = paddleDrag;
        playerR.view.GetComponent<Rigidbody2D>().drag = paddleDrag;

        float randAngle = Rand.value * Mathf.PI;

        var ballDirection = (new Vector2(Mathf.Cos(randAngle), -Mathf.Sin(randAngle)) * ballDirScale).normalized;

        ball.velocity = ballDirection * ballSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FixedUpdatePlayer(playerL, false);
        FixedUpdatePlayer(playerR, true);

        FixedUpdateEnvironment();
    }

    private void FixedUpdatePlayer(Player player, bool isRight)
    {
        var paddleView = player.view;

        var pos = paddleView.transform.position;
        pos.x = Mathf.Lerp(MinPaddleDistance, MaxPaddleDistance, paddleDistance) * ((isRight) ? 1.0f : -1.0f);

        paddleView.transform.position = pos;

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

    private void FixedUpdateEnvironment()
    {
        camera.orthographicSize = Mathf.Lerp(MinCameraScale, MaxCameraScale, paddleDistance);
        // TODO: Update Game Walls.
    }

    public void OnPaddleBallCollision(PaddleView view, Rigidbody2D ball)
    {
        // float maxAngle = Mathf.PI / 4.0f;
        var directionSquish = new Vector2(1.0f, 1.0f);

        var directionToBall = ball.transform.position - view.transform.position;
        Vector2 bounceDir = (directionSquish * directionToBall).normalized;

        ball.velocity = bounceDir * ballSpeed;
    }
}
