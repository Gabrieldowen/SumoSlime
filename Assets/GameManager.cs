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
            DontDestroyOnLoad(gameObject); // Preserve the GameManager across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
