using UnityEngine;

public class GameSetup : MonoBehaviour
{
    public GameObject realDungBeetlePrefab;
    public GameObject rolliePrefab;
    public GameObject ladybug2Prefab;
    public GameObject P2_RealDungBeetlePrefab;
    public GameObject P2_rolliePrefab;
    public GameObject P2_Ladybug2Prefab;

    private Vector3[] startPositions = new Vector3[]
    {

        new Vector3(8f, -4.0f, 2f), // Position for the first character
        new Vector3(-5.5f, -4.9f, -7.7f) // Position for the second character
    };

    private void Start()
    {
        // Instantiate the selected characters at their start positions
        for (int i = 0; i < GameManager.Instance.selectedCharacters.Length; i++)
        {
            string characterName = GameManager.Instance.selectedCharacters[i] + (i+1);
            print("getting prefab for " + characterName + "...");
            GameObject prefab = GetPrefabByName(characterName );

            // Set the player ID
            prefab.GetComponent<BugController>().playerID = i + 1;
            
            if (prefab != null && i < startPositions.Length)
            {
                GameObject characterInstance = Instantiate(prefab, startPositions[i], Quaternion.identity);
            }
        }
    }


    private GameObject GetPrefabByName(string characterName)
    {
        switch (characterName)
        {
            case "DungBeetle1":
                return realDungBeetlePrefab;
            case "RolyPoly1":
                return rolliePrefab;
            case "LadyBug1":
                return ladybug2Prefab;
            case "DungBeetle2":
                return P2_RealDungBeetlePrefab;
            case "RolyPoly2":
                return P2_rolliePrefab;
            case "LadyBug2":
                return P2_Ladybug2Prefab;
            default:
                Debug.LogError("CUSTOM ERROR Character name " + characterName + " is not recognized.");
                return null;
        }
    }
    
}
