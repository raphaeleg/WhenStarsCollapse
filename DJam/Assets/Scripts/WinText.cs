using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
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

    public GameObject title_timer;
    public GameObject preText_timer;
    public GameObject raw_timer;
    public GameObject title_stars;
    public GameObject preText_stars; 
    public GameObject raw_stars;
    public GameObject title_dwarf;
    public GameObject preText_dwarf;
    public GameObject raw_dwarf;
    public GameObject title_final;
    public GameObject final_btn;

    private WaitForSeconds delay = new WaitForSeconds(1);

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

        resultTimer.text = "0000";
        resultStars.text ="0000";
        resultWhiteDwarfs.text ="0000";
        totalResult.text = "0000";

        StartCoroutine(TimerAnim());
    }

    IEnumerator TimerAnim()
    {
        yield return delay;
        title_timer.SetActive(true);
        preText_timer.SetActive(true);
        yield return delay;
        raw_timer.SetActive(true);
        yield return delay;
        resultTimer.text = "" + resultTimerInt;
        StartCoroutine(StarsAnim());
    }

    IEnumerator StarsAnim()
    {
        yield return delay;
        title_stars.SetActive(true);
        preText_stars.SetActive(true);
        yield return delay;
        raw_stars.SetActive(true);
        yield return delay;
        resultStars.text = "" + resultStarsInt;
        StartCoroutine(DwarfPart());
    }
    IEnumerator DwarfPart()
    {
        yield return delay;
        title_dwarf.SetActive(true);
        preText_dwarf.SetActive(true);
        yield return delay;
        raw_dwarf.SetActive(true);
        yield return delay;
        resultWhiteDwarfs.text = "" + resultWhiteDwarfsInt;
        StartCoroutine(Total());
    }

    IEnumerator Total()
    {
        yield return delay;
        title_final.SetActive(true);
        yield return delay;
        totalResult.text = "" + (resultTimerInt + resultStarsInt + resultWhiteDwarfsInt);
        yield return delay;
        final_btn.SetActive(true);
    }
}
