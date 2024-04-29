using UnityEngine;

public class GameSetup : MonoBehaviour
{
    public GameObject realDungBeetlePrefab;
    public GameObject rolliePrefab;
    public GameObject ladybug2Prefab;

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
            string characterName = GameManager.Instance.selectedCharacters[i];
            print("getting prefab for " + characterName + "...");
            GameObject prefab = GetPrefabByName(characterName);

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
            case "DungBeetle":
                return realDungBeetlePrefab;
            case "RolyPoly":
                return rolliePrefab;
            case "LadyBug":
                return ladybug2Prefab;
            default:
                return null;
        }
    }
    
}
