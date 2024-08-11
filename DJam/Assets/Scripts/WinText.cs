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
    public GameObject mul_timer;
    public GameObject title_stars;
    public GameObject preText_stars; 
    public GameObject raw_stars;
    public GameObject mul_stars;
    public GameObject title_dwarf;
    public GameObject preText_dwarf;
    public GameObject raw_dwarf;
    public GameObject mul_dwarf;
    public GameObject title_final;
    public GameObject final_btn;

    private WaitForSeconds delay = new WaitForSeconds(0.5f);
    private WaitForSeconds delayHalf = new WaitForSeconds(0.2f);

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

        resultTimer.text = "";
        resultStars.text ="";
        resultWhiteDwarfs.text ="";
        totalResult.text = "";

        StartCoroutine(TimerAnim());
    }

    IEnumerator TextAnimation(TMP_Text text, int realScore)
    {
        float localTimer = 0.5f;
        int toCalc = 0;
        while (localTimer > 0)
        {
            localTimer -= 0.1f;
            toCalc += realScore/5;
            if (toCalc > realScore) { break; }
            text.text = "" + toCalc;
            yield return new WaitForSeconds(0.1f);
        }
        text.text = "" + realScore;
    }

    IEnumerator TimerAnim()
    {
        yield return delay;
        title_timer.SetActive(true);
        preText_timer.SetActive(true);
        mul_timer.SetActive(true);
        yield return delay;
        raw_timer.SetActive(true);
        yield return delay;
        StartCoroutine(TextAnimation(resultTimer, resultTimerInt));
        yield return delay;
        StartCoroutine(StarsAnim());
    }

    IEnumerator StarsAnim()
    {
        yield return delay;
        title_stars.SetActive(true);
        preText_stars.SetActive(true);
        mul_stars.SetActive(true);
        yield return delay;
        raw_stars.SetActive(true);
        yield return delay;
        StartCoroutine(TextAnimation(resultStars, resultStarsInt));
        yield return delay;
        StartCoroutine(DwarfPart());
    }
    IEnumerator DwarfPart()
    {
        yield return delay;
        title_dwarf.SetActive(true);
        preText_dwarf.SetActive(true);
        mul_dwarf.SetActive(true);
        yield return delay;
        raw_dwarf.SetActive(true);
        yield return delay;
        StartCoroutine(TextAnimation(resultWhiteDwarfs, resultWhiteDwarfsInt));
        yield return delay;
        StartCoroutine(Total());
    }

    IEnumerator Total()
    {
        yield return delay;
        title_final.SetActive(true);
        yield return delay;
        StartCoroutine(TextAnimation(totalResult, (resultTimerInt + resultStarsInt + resultWhiteDwarfsInt)));
        yield return delay;
        final_btn.SetActive(true);
    }
}
