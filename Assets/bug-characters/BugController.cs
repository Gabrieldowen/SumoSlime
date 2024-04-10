using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BugController : MonoBehaviour
{
    public float speed = 5.0f;
    private Vector2 moveDirection;

    private Rigidbody playerRB;
    private Vector3 startPOS;

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();

    }
    // Start is called before the first frame update
    void Start()
    {
        // when you start the game set the startPOS to wherever the players start at
        playerRB = this.GetComponent<Rigidbody>();
        startPOS = playerRB.position;
        print("start pos is set to: " + startPOS);

    }

    // Update is called once per frame
    void Update()
    {
        moveBug();
    }

    // actually moves the bug with rotation
    void moveBug(){
        // moves bug
        Vector3 movement = new Vector3(moveDirection.x, 0.0f, moveDirection.y);

        // rotates bug
        if(moveDirection != Vector2.zero){
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15F);
        }

        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter(Collider other){
        // if the bug hits the water, reset the position & velocity
        if(other.tag == "Water"){
            print("You hit the water!");
            resetGameState();
            // here you can reset the trail or update count of falling/deaths
        }
    }

    void resetGameState(){
        // reset the game state
        print("resetting position to: " + startPOS + "from position: " + playerRB.position);
        playerRB.position = startPOS;
        print("now position is: " + playerRB.position);
        playerRB.velocity = Vector3.zero;
        playerRB.angularVelocity = Vector3.zero;
        moveDirection = Vector2.zero;
        // here you can reset the trail or update count of falling/deaths
    }

}
