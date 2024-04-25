using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GlobalTime : MonoBehaviour
{
    private float timer = 10f;
    private TextMeshProUGUI timerSeconds;


    void Start()
    {
        timerSeconds = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        timer -= Time.deltaTime;
        timerSeconds.text = timer.ToString("f1");
        if (timer <= 0) {

            BugController[] bugControllers = FindObjectsOfType<BugController>();
            foreach (BugController bugController in bugControllers)
            {
                bugController.SaveScore();
            }

            int player1Score = PlayerPrefs.GetInt("Player_1_Score", 0);
            int player2Score = PlayerPrefs.GetInt("Player_2_Score", 0);
            if(player1Score > player2Score)
            {
                print("Player 1 Wins!: " + player1Score + " - " + player2Score);
            }
            else if(player2Score > player1Score)
            {
                print("Player 2 Wins!: "+ player1Score + " - " + player2Score);
            }
            else
            {
                print("Tie: " + player1Score + " - " + player2Score);
    
            }
            

            SceneManager.LoadScene("EndGame");
        }

    }


}
