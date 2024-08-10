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
        timer.text = highScore.timer.ToString();
        stars.text = highScore.stars.ToString();
        holes.text = highScore.blackHoles.ToString();
    }
}
