using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class BugController : MonoBehaviour
{
    public float speed = 8.0f;
    public int lives = 3;
    public float rotationEase = 0.1f;
    private Vector2 moveDirection;

    private Rigidbody playerRB;
    private Vector3 startPOS;

    // Trail
    private LineRenderer lineRenderer;
    private List<Vector3> points = new List<Vector3>();

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();

    }
    // Start is called before the first frame update
    void Start()
    {
        // when you start the game set the startPOS to wherever the players start at
        playerRB = this.GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();

        // get starting point
        startPOS = playerRB.position;
        print("start pos is set to: " + startPOS);

        // get init points for trail
        lineRenderer.positionCount = 1;
        points.Add(new Vector3(startPOS.x, 0f, startPOS.z));

    }

    // Update is called once per frame
    void Update()
    {
        moveBug();
        drawTrail();
        generateTrailMesh();
    }

    void generateTrailMesh(){
        MeshCollider collider = GetComponent<MeshCollider>();

        if (collider == null)
        {
            collider = gameObject.AddComponent<MeshCollider>();
        }

        collider.tag = "Trail";

        // Generate the mesh
        Mesh mesh = new Mesh();
        lineRenderer.BakeMesh(mesh, true);
        collider.sharedMesh = mesh;

    }

    void drawTrail(){
        // Draw the trail
        lineRenderer.positionCount = points.Count;
        for(int i = 0; i < points.Count; i++){
            lineRenderer.SetPosition(i, points[i]);
        }
    }

    // actually moves the bug with rotation
    void moveBug(){
        // moves bug
        Vector3 movement = new Vector3(moveDirection.x, 0.0f, moveDirection.y);

        // rotates bug
        if(moveDirection != Vector2.zero){
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), rotationEase);
        }
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        // update trail if moving 
        if(movement != Vector3.zero){
            points.Add(new Vector3(playerRB.position.x, 0f, playerRB.position.z));
        }
    }


    void OnTriggerEnter(Collider other){
        // if the bug hits the water, reset the position & velocity
        if(other.tag == "Water"){
            print("You hit the water!");
            resetGameState();
            // here you can reset the trail or update count of falling/deaths
        }
        if(other.tag == "Ground" && playerRB.velocity != Vector3.zero && playerRB.angularVelocity != Vector3.zero){
            // Nullify the force application when grounded
            playerRB.velocity = Vector3.zero;
            playerRB.angularVelocity = Vector3.zero;
            return; // Exit the Update method early to prevent applying additional force

        }
        if(other.tag == "Trail"){
            print("You hit the trail!");
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

        points.Clear();

        // here you can reset the trail or update count of falling/deaths
        lives--;
        if(lives <= 0){            
            // go to the game over screen
            SceneManager.LoadScene("EndGame");
            
        }
    }

}
