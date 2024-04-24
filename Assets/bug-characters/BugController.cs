using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class BugController : MonoBehaviour
{
    public float speed = 8.0f;
    public float rotationEase = 0.1f;
    private Vector2 moveDirection;

    private Rigidbody playerRB;
    private Vector3 startPOS;

    // Tilemap to be used to spawn blocks
    private Tilemap tileMap = null;

    // Tile to be used to fill the cells
    public TileBase filledCellTile;

    private AudioSource splashAudioSource;

    // Audio source to play when the bug hits the water
    public AudioSource audioSource;

    // Ground check variables
    private bool grounded = false;

    // trail amount
    private int trailCount = 0;

    private Bounds platformBounds;
    public int platformSize = 0;


    public void OnMove(InputAction.CallbackContext context)
    {
        // Input from user
        moveDirection = context.ReadValue<Vector2>();

    }
    // Start is called before the first frame update
    void Start()
    {
        // load in the splash sound
        splashAudioSource = GameObject.Find("SplashSound").GetComponent<AudioSource>();

        // when you start the game set the startPOS to wherever the players start at
        playerRB = this.GetComponent<Rigidbody>();
        startPOS = playerRB.position;
        print("start pos is set to: " + startPOS);
        tileMap = GameObject.Find("Tilemap").GetComponent<Tilemap>();

    }

    // void setPlatformSize(){

    //     Bounds bounds = GameObject.FindGameObjectWithTag("Ground").GetComponent<BoxCollider>().bounds;


    //     // Calculate the corners
    //     Vector3Int topLeft = tileMap.WorldToCell(bounds.center + new Vector3(-bounds.extents.x, bounds.extents.y, bounds.extents.z));
    //     Vector3Int topRight = tileMap.WorldToCell(bounds.center + new Vector3(bounds.extents.x, bounds.extents.y, bounds.extents.z));
    //     Vector3Int bottomLeft = tileMap.WorldToCell(bounds.center + new Vector3(-bounds.extents.x, bounds.extents.y, -bounds.extents.z));
    //     Vector3Int bottomRight = tileMap.WorldToCell(bounds.center + new Vector3(bounds.extents.x, bounds.extents.y, -bounds.extents.z));

    //     int side1 = Mathf.Abs(topLeft.x - topRight.x) + Mathf.Abs(topLeft.y - topRight.y);
    //     int side2 = Mathf.Abs(bottomLeft.x - bottomRight.x) + Mathf.Abs(bottomLeft.y - bottomRight.y);
    //     platformSize = side1 * side2;

    // }

    // Update is called once per frame
    void Update()
    {

        moveBug();
        SpawnSlime();
    }


    // actually moves the bug with rotation
    void moveBug(){
        // moves bug
        Vector3 movement = new Vector3(moveDirection.x, 0.0f, moveDirection.y);

        // rotates bug
        if(moveDirection != Vector2.zero){
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), rotationEase);
        }

        // Transfrom character position based on new information
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter(Collider other){


        // if the bug hits the water, reset the position & velocity
        if(other.tag == "Water"){
            print("You hit the water!");
            splashAudioSource.Play();
            resetGameState();
            // here you can reset the trail or update count of falling/deaths
        }
        

    }
    void OnCollisionEnter(Collision collision){
        if(!grounded && collision.gameObject.tag == "Ground"){
            grounded = true;

            // when the player hits the ground, stop all movement
            playerRB.velocity = Vector3.zero;
            playerRB.angularVelocity = Vector3.zero;
        }
    }
    void OnCollisionExit(Collision collision){
        if(collision.gameObject.tag == "Ground"){
            grounded = false;
        }
    }

    void resetGameState(){
        // reset the game state
        playerRB.position = startPOS;
        playerRB.velocity = Vector3.zero;
        playerRB.angularVelocity = Vector3.zero;
        moveDirection = Vector2.zero;
        trailCount = 0;

        BoundsInt bounds = tileMap.cellBounds;
        TileBase[] allTiles = tileMap.GetTilesBlock(bounds); // Get all tiles within the bounds

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                for (int z = bounds.zMin; z < bounds.zMax; z++)
                {
                    Vector3Int cellPosition = new Vector3Int(x, y, z);
                    TileBase tile = tileMap.GetTile(cellPosition);

                    if (tile == filledCellTile)
                    {
                        tileMap.SetTile(cellPosition, null);
                    }
                }
            }
        }


    }
    private void SpawnSlime()
    {
        if(grounded){
            // Set the tile at the grid position to be the filled cell tile
            Vector3Int gridPosition = tileMap.WorldToCell(transform.position);

            if(tileMap.GetTile(gridPosition) != filledCellTile){
                 // set the tile at the grid position to be the filled cell tile
                tileMap.SetTile(gridPosition, filledCellTile);
                trailCount++;
            }

        }
    }

}

