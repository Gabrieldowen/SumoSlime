using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RPBugController : MonoBehaviour
{
    public float speed = 5.0f;

    private float x;
    
    private float rotationSpeed;

    private Vector2 moveDirection;

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();

    }
    // Start is called before the first frame update, initialize some used variables
    void Start()
    {
        x = 0.0f;
        rotationSpeed = 75.0f;
    }

    // Update is called once per frame
    void Update()
    {
        moveBug();
    }

    // actually moves the bug with rotation
    void moveBug(){
        Vector3 movement = new Vector3(moveDirection.x, 0.0f, moveDirection.y);

        if(moveDirection != Vector2.zero){
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15F);
            x += Time.deltaTime * rotationSpeed;
            if (x > 360.0f)
            {
                x = 0.0f;
            }
        }

        transform.Translate(movement * speed * Time.deltaTime, Space.World);
        transform.localRotation = Quaternion.Euler(x, 0, 0);
    }

}