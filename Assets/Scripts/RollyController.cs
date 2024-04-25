using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RollyController : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    private Vector2 moveDirection;
    private Collider playerCollider;

    private Rigidbody playerRB;
    private Vector3 startPOS;
    private bool isMoving = false;

    public float rollSpeed = 3.0f;

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
        playerCollider = GetComponent<Collider>(); // Assuming the player has a collider component
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
        if(isMoving)
            return;
        moveBug();
    }

    // actually moves the bug with rotation
    void moveBug(){
        // moves bug
        Vector3 movement = new Vector3(moveDirection.x, 0.0f, moveDirection.y);

        //rotates bug
        if(moveDirection != Vector2.zero){

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15F); 
   
        }

        //actually moves the bug
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        // // roll the bug
        // if(moveDirection != Vector2.zero){

        //      var anchor = playerCollider.bounds.center;
        //      var axis = Vector3.Cross(Vector3.up, movement);

        //      print("\npos "+transform.position+"anchor " + anchor + "axis " + axis);

        //      // StartCoroutine(rollBug(anchor, axis));
        //  }
    }

    IEnumerator rollBug(Vector3 anchor, Vector3 axis){
        isMoving = true;
        for(int i = 0; i<(90/rollSpeed); i++){
            transform.RotateAround(anchor, axis, 3);
            yield return new WaitForSeconds(0.1f);
        }
        isMoving = false;
    }

    void OnTriggerEnter(Collider other){
        // if the bug hits the water, reset the position & velocity
        if(other.tag == "Water"){
            print("You hit the water!");
            resetGameState();
            // here you can reset the trail or update count of falling/deaths
        }
        if(other.tag == "Ground"){
            // Nullify the force application when grounded
            playerRB.velocity = Vector3.zero;
            playerRB.angularVelocity = Vector3.zero;
            return; // Exit the Update method early to prevent applying additional force

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
