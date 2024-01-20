using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerController playerController;

    private int horizontal = 0, vertical = 0;

    private enum Axis{
        Horizontal,
        Vertical
    }
    // Start is called before the first frame update
    void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = 0;
        vertical = 0;
        GetKeyboardInput();
        SetMovement();
    }

    void GetKeyboardInput(){
        horizontal = GetAxis(Axis.Horizontal);
        vertical = GetAxis(Axis.Vertical);

        if(horizontal != 0){
            vertical = 0;
        }
    }

    void SetMovement(){
        if(vertical!=0){
            playerController.SetInputDirection((vertical == 1)? PlayerDirection.UP : PlayerDirection.DOWN);
        }
        else if(horizontal != 0){
            playerController.SetInputDirection((horizontal == 1)? PlayerDirection.RIGHT : PlayerDirection.LEFT);
        }
    }

    int GetAxis(Axis axis){
        if(axis == Axis.Horizontal){
            bool leftArrow = Input.GetKeyDown(KeyCode.LeftArrow);
            bool leftA = Input.GetKeyDown(KeyCode.A);
            bool rightArrow = Input.GetKeyDown(KeyCode.RightArrow);
            bool rightD = Input.GetKeyDown(KeyCode.D);
            if(leftArrow == true || leftA == true){
                return -1;
            }
            if(rightArrow == true || rightD == true){
                return 1;
            }
            return 0;
        }
        
        else if(axis == Axis.Vertical){
            bool upArrow = Input.GetKeyDown(KeyCode.UpArrow);
            bool upW= Input.GetKeyDown(KeyCode.W);
            bool downArrow = Input.GetKeyDown(KeyCode.DownArrow);
            bool downS = Input.GetKeyDown(KeyCode.S);
            if(upArrow == true || upW == true){
                return 1;
            }
            if(downArrow == true || downS == true){
                return -1;
            }
            return 0;
        }
        return 0;
    }
}
