using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TrailController : MonoBehaviour
{
    public float speed = 5.0f;
    private Vector2 moveDirection;
     public Transform trailPosition;
    public GameObject trailPrefab;

    // Update is called once per frame
    void LeaveTrail()
    {
        // get the trail just behind the character
        Vector3 backwardDirection = -moveDirection.normalized;
        Vector3 trailBackwardPosition = trailPosition.position + backwardDirection*1.5f; // Adjust the distance as needed
        Instantiate(trailPrefab, trailBackwardPosition, trailPosition.rotation);
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();

    }
    // Start is called before the first frame update
    void Start()
    {
        
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
            LeaveTrail();
        }

        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }

}

