using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TextMeshProUGUI gameScore1Text;
    public TextMeshProUGUI gameScore2Text;

    private int gameScore1 = 0;
    private int gameScore2 = 0;

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
        gameScore1 = score;
        if (gameScore1Text != null)
            gameScore1Text.text = "Game Score 1: " + score;
    }

    public void UpdateGameScore2(int score)
    {
        gameScore2 = score;
        if (gameScore2Text != null)
            gameScore2Text.text = "Game Score 2: " + score;
    }

    public string GetWinner()
    {
        // Logic to determine the winner based on scores
        if (gameScore1 > gameScore2)
            return "Player 1";
        else if (gameScore2 > gameScore1)
            return "Player 2";
        else
            return "It's a tie!";
    }


}
