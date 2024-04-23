using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public string[] selectedCharacters = new string[2];
    public string selectedMap;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Set default values if they're empty or null
            if (selectedCharacters[0] == null || selectedCharacters[0] == "")
            {
                selectedCharacters[0] = "DungBeetle"; // Default character
            }
            if (selectedCharacters[1] == null || selectedCharacters[1] == "")
            {
                selectedCharacters[1] = "LadyBug"; // Default character
            }
            if (string.IsNullOrEmpty(selectedMap))
            {
                selectedMap = "FlatMap"; // Default map
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
