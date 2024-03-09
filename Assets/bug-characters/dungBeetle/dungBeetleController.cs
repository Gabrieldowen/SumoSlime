using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BugController : MonoBehaviour
{
    public float speed = 5.0f;
    private Vector2 moveDirection;

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

    void moveBug(){
        Vector3 movement = new Vector3(moveDirection.x, 0.0f, moveDirection.y);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15F);

        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }
}
