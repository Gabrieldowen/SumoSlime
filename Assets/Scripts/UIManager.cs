using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TextMeshProUGUI gameScore1Text;
    public TextMeshProUGUI gameScore2Text;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep UIManager across scenes if needed
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateGameScore1(int score)
    {
        Debug.Log("Updating GameScore1: " + score);
        if (gameScore1Text != null)
            gameScore1Text.text = "Player 1: \n" + score;
    }

    public void UpdateGameScore2(int score)
    {
        Debug.Log("Updating GameScore2: " + score);
        if (gameScore2Text != null)
            gameScore2Text.text = "Player 2: \n" + score;
    }


}
