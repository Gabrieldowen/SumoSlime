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

        timerSeconds.text = timer.ToString("f1");
    }
    void Update()
    {
        timer -= Time.deltaTime;
        timerSeconds.text = timer.ToString("f1");

        if (timer <= 0) {

            SceneManager.LoadScene("EndGame");
        }

    }



}
