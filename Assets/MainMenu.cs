using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour {


    public void playGame() {
        string mapToLoad = GameManager.Instance.selectedMap;
        UnityEngine.SceneManagement.SceneManager.LoadScene(mapToLoad);
    }
    public void QuitGame() {
        Debug.Log("QUIT!");
        Application.Quit();
    }

        
}
