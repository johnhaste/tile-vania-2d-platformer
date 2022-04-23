using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    float playerGravity;

    Vector2 moveInput;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        //Gets current gravity
        playerGravity = myRigidBody.gravityScale;
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider  = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Always updates the player horizontal velocity
        Run();
        ClimbLadder();
        FlipSprite();
    }

    void ClimbLadder()
    {
        if(myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))){
            //When the player is climbing it shouldn't fall
            myRigidBody.gravityScale = 0;
            //Updates velocity according to the input
            Vector2 climbVelocity = new Vector2 (myRigidBody.velocity.x, moveInput.y * climbSpeed);
            myRigidBody.velocity = climbVelocity;
            //Updates animation
            myAnimator.SetBool("isClimbing", true);
        }else{
             //Updates animation
            myAnimator.SetBool("isClimbing", false);
            //When the player is NOT climbing it  fall
            myRigidBody.gravityScale = playerGravity;
        }
        
    }

    void OnMove(InputValue value){
        //It's triggered by the arrows
        moveInput = value.Get<Vector2>();
    }

    void Run()
    {
        //Updates velocity according to the input
        Vector2 playerVelocity = new Vector2 (moveInput.x * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        //Updates animator (Could also be myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);)
        myAnimator.SetBool("isRunning", true);
    }

    

    void OnJump(InputValue value){

        //If not touching the groud
        if(!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){

        }else{
            if(value.isPressed){
                myRigidBody.velocity += new Vector2(0f, jumpSpeed);
            }
        }
    }

    void FlipSprite(){

        //Checks if player is moving horizontally
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        
        //If it's running
        if(playerHasHorizontalSpeed){
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }else{
            myAnimator.SetBool("isRunning", false);
        }
    }


}
