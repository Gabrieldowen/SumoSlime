using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndGameMenu : MonoBehaviour
{

    public TextMeshProUGUI winnerText; // Assign this in the inspector

    void Start()
    {
        if (UIManager.Instance != null)
        {
            string winner = UIManager.Instance.GetWinner();
            if (winnerText != null)
                winnerText.text = "Winner: " + winner;
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex) - 2);
    }


}