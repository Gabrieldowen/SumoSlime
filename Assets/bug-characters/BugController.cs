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
    [Header("Slime Types")]
    [SerializeField] private GameObject slimePrefab;

    // Initialize the starting time of the slime spawner
    [Header("Spawn System")]
    [SerializeField] private float spawnTimer = 0f;

    // Tilemap to be used to spawn blocks
    public Tilemap tileMap = null;

    // Tile to be used to fill the cells
    public TileBase filledCellTile = null;

    public AudioSource audioSource;
    public void OnMove(InputAction.CallbackContext context)
    {
        // Input from user
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
        // Timer for spawning tiles
        spawnTimer += Time.deltaTime;
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
            audioSource.Play();
            resetGameState();
            // here you can reset the trail or update count of falling/deaths
        }
        if(other.tag == "Ground" && playerRB.velocity != Vector3.zero && playerRB.angularVelocity != Vector3.zero){
            // Nullify the force application when grounded
            playerRB.velocity = Vector3.zero;
            playerRB.angularVelocity = Vector3.zero;
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
        
        // loop through the tile map and remove all the filled cells of that player
        print("looping through the tile map" + tileMap.size.x +"x"+tileMap.size.y);
        for (int x = -(int)(tileMap.size.x/2 -1); x < tileMap.size.x; x++)
        {
            for (int y = -(int)(tileMap.size.y/2 -1); y < tileMap.size.y; y++)
            {
                // Get the tile at the current position
                Vector3Int pos = tileMap.WorldToCell(new Vector3(x, 0, y));
                TileBase tile = tileMap.GetTile(pos);

                // If the tile is the filled cell tile, remove it
                if (tile == filledCellTile)
                {
                    tileMap.SetTile(pos, null);
                }
            }
        }

    }
    private void SpawnSlime()
    {
        // Set the tile at the grid position to be the filled cell tile
        Vector3Int gridPosition = tileMap.WorldToCell(transform.position);

        // set the tile at the grid position to be the filled cell tile
        tileMap.SetTile(gridPosition, filledCellTile);
    }

}
