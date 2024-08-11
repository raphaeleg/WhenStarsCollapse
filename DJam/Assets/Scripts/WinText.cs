using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinText : MonoBehaviour
{
    public HighScore highScore;

    public TMP_Text timerMin;
    public TMP_Text timerSec;
    public TMP_Text stars;
    public TMP_Text whiteDwarf;

    public TMP_Text resultTimer;
    public TMP_Text resultStars;
    public TMP_Text resultWhiteDwarfs;
    public TMP_Text totalResult;

    private int resultTimerInt;
    private int resultStarsInt;
    private int resultWhiteDwarfsInt;

    private const int multiplierTime = 100;
    private const int multiplierStars = 200;
    private const int multiplierWhiteDwarfs = 250;

    void Start()
    {
        int minute = highScore.timer / 60;
        int second = highScore.timer % 60;
        string minuteStr = minute.ToString();
        if (minuteStr.Length == 1)
            minuteStr = "0" + minuteStr;
        string secondStr = second.ToString();
        if (secondStr.Length == 1)
            secondStr = "0" + secondStr;

        timerMin.text = minuteStr;
        timerSec.text = secondStr;
        stars.text = ""+highScore.stars;
        whiteDwarf.text = ""+highScore.whiteDwarfs;

        resultTimerInt = (highScore.timer * multiplierTime);
        resultStarsInt = (highScore.stars * multiplierStars);
        resultWhiteDwarfsInt = (highScore.whiteDwarfs * multiplierWhiteDwarfs);

        resultTimer.text ="" + resultTimerInt;
        resultStars.text ="" + resultStarsInt;
        resultWhiteDwarfs.text ="" + resultWhiteDwarfsInt;

        totalResult.text = "" + (resultTimerInt + resultStarsInt + resultWhiteDwarfsInt);
    }


}
