using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreText : MonoBehaviour
{
    public HighScore highScore;

    public TMP_Text timer;
    public TMP_Text stars;
    public TMP_Text holes;

    void Update()
    {
        int minute = highScore.timer / 60;
        int second = highScore.timer % 60;
        string minuteStr = minute.ToString();
        if (minuteStr.Length == 1)
            minuteStr = "0" + minuteStr;
        string secondStr = second.ToString();
        if (secondStr.Length == 1)
            secondStr = "0" + secondStr;
        timer.text = minuteStr + ":" + secondStr;
        stars.text = highScore.stars.ToString();
        holes.text = highScore.blackHoles.ToString();
    }
}
