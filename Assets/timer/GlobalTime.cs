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
    }

    //public GameObject timeDisplay;
    //public int seconds = 30;
    //public bool deductTime;


    //// Update is called once per frame
    //void Update()
    //{
    //    if (deductTime == false)
    //    {
    //        deductTime = true;
    //        StartCoroutine(deductSecond());
    //    }
    //}

    //IEnumerator deductSecond()
    //{
    //    yield return new WaitForSeconds(1);
    //    seconds -= 1;
    //    timeDisplay.GetComponent<Text>().text = "TIME: " + seconds;
    //    deductTime = false;
    //}

}
