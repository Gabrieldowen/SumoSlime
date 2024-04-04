using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalTime : MonoBehaviour
{
    private float timer = 10f;
    private Text timerSeconds;


    void Start()
    {
        timerSeconds = GetComponent<Text>();
    }
    void Update()
    {
        timer -= Time.deltaTime;
        timerSeconds.text = timer.ToString("f0");
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
