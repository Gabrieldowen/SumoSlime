using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

using UnityEngine.Tilemaps;

public class BugController : MonoBehaviour
{
    public float speed = 8.0f;
    public int lives = 3;
    public float rotationEase = 0.1f;
    private Vector2 moveDirection;

    private Rigidbody playerRB;
    private Vector3 startPOS;
    [Header("Slime Types")]
    [SerializeField] private GameObject slimePrefab;

    [Header("Spawn System")]
    [SerializeField] private float spawnTimer = 0f;
    public Tilemap tileMap = null;
    //public List<Vector3> availablePlaces;

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

        tileMap = transform.parent.GetComponent<Tilemap>();
        /*availablePlaces = new List<Vector3>();

        // Iterate through the tilemap cells
        for (int n = tileMap.cellBounds.xMin; n < tileMap.cellBounds.xMax; n++)
        {
            for (int p = tileMap.cellBounds.yMin; p < tileMap.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = new Vector3Int(n, p, (int)tileMap.transform.position.y);
                Vector3 place = tileMap.CellToWorld(localPlace);

                if (tileMap.HasTile(localPlace))
                {
                    // Tile at "place" is available for spawning
                    availablePlaces.Add(place);
                }
                else
                {
                    // No tile at "place"
                }
            }
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        moveBug();
    }

    private void FixedUpdate()
    {
        // Spawn slimes periodically
        InvokeRepeating("SpawnSlime", spawnTimer, 0.05f);
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
        lives--;
        if(lives <= 0){            
            // go to the game over screen
            SceneManager.LoadScene("EndGame");
        }
    }
    private void SpawnSlime()
    {
        if (spawnTimer > 0.25)
        {
            // Instantiate a slime at a random available position
            //int randomIndex = Random.Range(0, availablePlaces.Count);
            //Vector3 spawnPosition = availablePlaces[randomIndex];
            // Simulate character movement (you can replace this with your actual movement logic)
            Vector3 characterPosition = transform.position;
            Instantiate(slimePrefab, characterPosition, Quaternion.identity);

            spawnTimer = 0f;
        }
    }

}
