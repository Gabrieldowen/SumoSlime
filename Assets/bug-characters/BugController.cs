using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class BugController : MonoBehaviour
{
    // Character speed
    public float speed = 5.0f;

    // Character input for moving
    private Vector2 moveDirection;

    // Slime to be used with character
    [Header("Slime Types")]
    [SerializeField] private GameObject slimePrefab;

    // Initialize the starting time of the slime spawner
    [Header("Spawn System")]
    [SerializeField] private float spawnTimer = 0f;

    // Tilemap to be used to spawn blocks
    public Tilemap tileMap = null;
    //public List<Vector3> availablePlaces;

    public void OnMove(InputAction.CallbackContext context)
    {
        // Input from user
        moveDirection = context.ReadValue<Vector2>();

    }
    // Start is called before the first frame update
    void Start()
    {
        // Initializes tilemap
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
        // Timer for spawning tiles
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
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15F);
        }

        // Transfrom character position based on new information
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }

    private void SpawnSlime()
    {
        //Vector3Int gridPosition = tileMap.WorldToCell(transform.position + (Vector3)moveDirection);
        // Ever 0.15 seconds, spawn a slime
        if (spawnTimer > 0.15)// && tileMap.HasTile(gridPosition))
        {
            // Instantiate a slime at a random available position
            //int randomIndex = Random.Range(0, availablePlaces.Count);
            //Vector3 spawnPosition = availablePlaces[randomIndex];
            // Simulate character movement
            // Spot on grid to place slime, only int positions are allowed on grid for easy mapping
            Vector3 characterGridPosition = new Vector3((int)(transform.position.x), transform.position.y, (int)(transform.position.z));

            // Spawns block
            Instantiate(slimePrefab, characterGridPosition, Quaternion.identity);

            spawnTimer = 0f;
        }
    }

}
