using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionKnockback : MonoBehaviour
{
    private float knockbackForce = 17;

    private void OnCollisionEnter(Collision collision)
    {
        // Get the rigidbody of the collider we hit
        Rigidbody rb = collision.collider.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Get the direction from the hitter to the collided object
            Vector3 direction = collision.transform.position - transform.position;
            direction.y = 0.5f; // Adjust the y component if needed

            // Check the direction the hitter is facing
            Vector3 hitterForward = transform.forward;

            // Dot product to determine if the collided object is in front of the hitter
            float dot = Vector3.Dot(direction.normalized, hitterForward);
            
            // If the collided object is in front of the hitter
            if (dot > 0)
            {
                rb.AddForce(direction.normalized * knockbackForce, ForceMode.Impulse);
            }
        }
    }
}
