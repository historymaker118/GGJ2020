using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayer(playerL);
        UpdatePlayer(playerR);
    }

    private void UpdatePlayer(Player player)
    {
        switch (player.controllerType)
        {
            case ControllerType.KeyboardLeft:
                if (Input.GetKey(KeyCode.W))
                {
                    player.view.MoveUp();
                }
                if (Input.GetKey(KeyCode.S))
                {
                    player.view.MoveDown();
                }
                break;
            case ControllerType.KeyboardRight:
                if (Input.GetKey(KeyCode.I))
                {
                    player.view.MoveUp();
                }
                if (Input.GetKey(KeyCode.K))
                {
                    player.view.MoveDown();
                }
                break;
            case ControllerType.Controller:
                var axis = Input.GetAxisRaw("Vertical");
                if (axis > 0.0f)
                {
                    player.view.MoveUp();
                }
                if (axis < 0.0f)
                {
                    player.view.MoveDown();
                }
                break;
        }
    }
}
