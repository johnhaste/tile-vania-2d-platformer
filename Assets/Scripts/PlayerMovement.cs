using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;

    Vector2 moveInput;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider  = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Always updates the player horizontal velocity
        Run();
        FlipSprite();
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
