using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    Vector2 moveInput;
    Rigidbody2D myRigidBody;

    Animator myAnimator;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
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
