using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndGameMenu : MonoBehaviour
{
    private TextMeshProUGUI winnerText;

    public void Start() {
        winnerText = GetComponent<TextMeshProUGUI>();

        BugController[] bugControllers = FindObjectsOfType<BugController>();
        foreach (BugController bugController in bugControllers)
        {
            bugController.SaveScore();
        }

        int player1Score = PlayerPrefs.GetInt("Player_1_Score", 0);
        int player2Score = PlayerPrefs.GetInt("Player_2_Score", 0);
        if(player1Score > player2Score)
        {
            winnerText.text = "Player 1 Wins! " + player1Score + " - " + player2Score;
        }
        else if(player2Score > player1Score)
        {
            winnerText.text = "Player 2 Wins! " + player2Score + " - " + player1Score;
        }
        else
        {
            winnerText.text = "Tie! " + player1Score + " - " + player2Score;

        }
    }
    public void MainMenu() {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex) -2);
    }


}
