using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class BugController : MonoBehaviour
{
    private float speed = 12.0f;
    public float rotationEase = 0.1f;
    private Vector2 moveDirection;

    private Rigidbody playerRB;
    // Rollie object for spinning reasons
    private GameObject rollie;
    private GameObject rollie2;
    private Vector3 startPOS;

    // Tilemap to be used to spawn blocks
    public Tilemap tileMap = null;

    // Tile to be used to fill the cells
    public TileBase filledCellTile;

    private AudioSource splashAudioSource;

    // Audio source to play when the bug hits the water
    public AudioSource audioSource;

    // Ground check variables
    private bool grounded = false;

    // trail amount
    private int trailCount = 0;

    public int playerID;

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
        rollie = GameObject.Find("rolliePrefab(Clone)");
        rollie2 = GameObject.Find("P2_rolliePrefab(Clone)");
        startPOS = playerRB.position;
        tileMap = GameObject.Find("Tilemap").GetComponent<Tilemap>();

    }

    // Update is called once per frame
    void Update()
    {

        moveBug();
        SpawnSlime();
        updateTrailCount();
    }


    // actually moves the bug with rotation
    void moveBug(){
        //if(grounded){
            // moves bug
            Vector3 movement = new Vector3(moveDirection.x, 0.0f, moveDirection.y);

            // Calculate the rolling forward vector (along the X-axis)
            Vector3 rollingForward = Vector3.right;

            // Rotate the bug around its local X-axis
            float rollingAngle = 1f; // Adjust the rolling speed as needed

            // rotates bug
            if(moveDirection != Vector2.zero){      // Determines if there is movement input, do this
                if(rollie != null && playerRB == rollie.GetComponent<Rigidbody>() || rollie2 != null && playerRB == rollie2.GetComponent<Rigidbody>()){               // Determines if this model is the rollie
                    transform.Rotate(Vector3.right, rollingAngle * (float)6);   // Spin the rollie
                }
                
                // Rotate the character to face the way they are moving
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), rotationEase);
            }

            // Transfrom character position based on new information
            transform.Translate(movement * speed * Time.deltaTime, Space.World);
       // }
    }

    void OnTriggerEnter(Collider other){


        // if the bug hits the water, reset the position & velocity
        if(other.tag == "Water"){
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
                updateTrailCount();

            }

        }
    }

    private void updateTrailCount(){
        BoundsInt bounds = tileMap.cellBounds;
        TileBase[] allTiles = tileMap.GetTilesBlock(bounds); // Get all tiles within the bounds

        int newTrailCount = 0;

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
                        newTrailCount++;
                    }
                }
            }
        }

        trailCount = newTrailCount;

        if (playerID == 1)
            UIManager.Instance.UpdateGameScore1(trailCount);
        else
            UIManager.Instance.UpdateGameScore2(trailCount);
    }

    public void SaveScore(){
        PlayerPrefs.SetInt("Player_"+playerID+"_Score", trailCount);
    }

}

