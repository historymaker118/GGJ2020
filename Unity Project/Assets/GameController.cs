using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

[Serializable]
public struct Environment
{
    public BoxCollider2D topWall;
    public BoxCollider2D bottomWall;
    public BoxCollider2D leftWall;
    public BoxCollider2D rightWall;
}

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public Action<float> onPaddleDistance;

    public bool demoMode = false;
    
    private float MinPaddleDistance = 3.0f;
    private float MaxPaddleDistance = 40.0f;
    
    private float MinCameraScale = 3.0f;
    private float MaxCameraScale = 25.0f;
    
    private Vector2 MinWallPos = new Vector2(5.32f, 3.37f);
    private Vector2 MaxWallPos = new Vector2(42.0f, 12.0f);

    public Player playerL;
    public Player playerR;

    public BallView ball;

    public Environment env;

    new public Camera camera;

    public HeartParticles particles;

    public AnimationCurve particlesAlpha;

    private float paddleSpeed = 4.0f;
    private float paddleDrag = 20.0f;

    private float ballSpeed = 14.0f;

    [Range(-0.5f, 0.5f)]
    public float decay = 0.01f;

    [SerializeField, Range(0.0f, 1.0f)]
    private float paddleDistance = 0.5f;

    private float penalty = 0.1f;
    private float reward = -0.05f;

    private float decayEffect = 0.0f;

    //private float winDistance = 0.03f;

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

    private void OnDestroy()
    {
        instance = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerL.view.GetComponent<Rigidbody2D>().drag = paddleDrag;
        playerR.view.GetComponent<Rigidbody2D>().drag = paddleDrag;

        if (!demoMode)
            ShootBall();
    }

    private void Update()
    {
        decayEffect = Mathf.Lerp(decayEffect, 0, Time.deltaTime / 2.0f);
        paddleDistance = Mathf.Clamp01(paddleDistance + (decay + decayEffect) * Time.deltaTime);

        onPaddleDistance.Invoke(paddleDistance);
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

        var wallPos = Vector2.Lerp(MinWallPos, MaxWallPos, paddleDistance);

        env.topWall.transform.position = Vector3.up * wallPos.y;
        env.bottomWall.transform.position = Vector3.down * wallPos.y;

        env.leftWall.transform.position = Vector3.left * wallPos.x;
        env.rightWall.transform.position = Vector3.right * wallPos.x;
    }

    public void ResetBall()
    {
        ShootBall();

        decayEffect += penalty;
    }

    private void ShootBall()
    {
        float randAngle = Rand.Range(-Mathf.PI / 4.0f, Mathf.PI / 4.0f);

        var ballDirection = new Vector2(Mathf.Cos(randAngle), -Mathf.Sin(randAngle));

        ballDirection.x *= (Rand.value > 0.5f) ? -1.0f : 1.0f;

        ball.Rigid.position = Vector2.zero;
        ball.Rigid.velocity = ballDirection * ballSpeed;
    }

    public void OnPaddleBallCollision(PaddleView view, Rigidbody2D ball)
    {
        // float maxAngle = Mathf.PI / 4.0f;
        var directionSquish = new Vector2(1.0f, 0.5f);

        var directionToBall = ball.transform.position - view.transform.position;
        Vector2 bounceDir = (directionSquish * directionToBall).normalized;

        ball.velocity = bounceDir * ballSpeed;

        decayEffect = reward;
    }
}
