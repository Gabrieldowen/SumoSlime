using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour {


    public void playGame()
    {
        if (string.IsNullOrEmpty(GameManager.Instance.selectedMap))
        {
            Debug.LogError("Selected map is null or empty. Loading default map.");
            GameManager.Instance.selectedMap = "FlatMap"; // Fallback to default map
        }

        SceneManager.LoadScene(GameManager.Instance.selectedMap);
    }
    public void QuitGame() {
        Debug.Log("QUIT!");
        Application.Quit();
    }

        
}
