using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] Score highScore;
    private const int MULTIPLIER_TIME = 100;
    private const int MULTIPLIER_STARS = 200;
    private const int MULTIPLIER_WHITEDWARFS = 250;

    public List<AnimationData> animationList;
    [SerializeField] GameObject CTA;

    private const float DELAY = 0.5f;
    private const float DELAY_SECTION = DELAY*4;

    [Serializable]
    public struct AnimationData
    {
        [Header("Constant Text")]
        public GameObject Title;
        public GameObject Label;
        public GameObject MulLabel;
        [Header("Base Value")]
        public GameObject BaseValObj;
        private TMP_Text BaseValStr;
        [Header("Score Value")]
        public GameObject ScoreValObj;
        public int ScoreVal { get; private set; }
        public TMP_Text ScoreValStr { get; private set; }

        public AnimationData(GameObject t, GameObject l, GameObject mL, GameObject bVS, GameObject sVS)
        {
            Title = t;
            Label = l;
            MulLabel = mL;
            BaseValObj = bVS;
            BaseValStr = bVS ? bVS.GetComponent<TMP_Text>() : null;
            ScoreValObj = sVS;
            ScoreVal = 0;
            ScoreValStr = sVS.GetComponent<TMP_Text>();
            ScoreValStr.text = "";
        }

        public AnimationData PassDynamicData(int baseVal, int mul, bool isTime = false)
        {
            if (BaseValObj)
            {
                if (isTime)
                {
                    TMP_Text minutes = BaseValObj.transform.GetChild(0).GetComponent<TMP_Text>();
                    TMP_Text seconds = BaseValObj.transform.GetChild(1).GetComponent<TMP_Text>();
                    minutes.text = FormatTimeText(baseVal / 60);
                    seconds.text = FormatTimeText(baseVal % 60);
                }
                else
                {
                    BaseValStr = BaseValObj.GetComponent<TMP_Text>();
                    if (BaseValStr) { BaseValStr.text = baseVal.ToString(); }
                }
            }

            if (MulLabel)
            {
                TMP_Text mulValue = MulLabel.transform.GetChild(1).GetComponent<TMP_Text>();
                mulValue.text = mul.ToString();
            }

            ScoreValStr = ScoreValObj.GetComponent<TMP_Text>();
            ScoreVal = baseVal * mul;
            return this;
        }

        private readonly string FormatTimeText(int val)
        {
            string str = val.ToString();
            if (str.Length == 1) { str = "0" + str; }
            return str;
        }
    };

    void Start()
    {
        animationList[0] = animationList[0].PassDynamicData(highScore.time, MULTIPLIER_TIME, true);
        animationList[1] = animationList[1].PassDynamicData(highScore.stars, MULTIPLIER_STARS);
        animationList[2] = animationList[2].PassDynamicData(highScore.whiteDwarfs, MULTIPLIER_WHITEDWARFS);
        
        int val = 0;
        foreach (AnimationData data in animationList) { val += data.ScoreVal; }
        animationList[^1] = animationList[^1].PassDynamicData(val, 1);

        StartCoroutine(Sequence());
    }

    private IEnumerator Sequence()
    {
        foreach (AnimationData data in animationList)
        {
            StartCoroutine(Sequence(data));
            yield return new WaitForSeconds(DELAY_SECTION);
        }

        CTA.SetActive(true);
    }

    IEnumerator Sequence(AnimationData data)
    {
        yield return new WaitForSeconds(DELAY);
        EventManager.TriggerEvent("SFX_ScoreAppear", 0);
        data.Title.SetActive(true);
        if (data.Label) { data.Label.SetActive(true); }
        if (data.MulLabel) { data.MulLabel.SetActive(true); }
        yield return new WaitForSeconds(DELAY);
        EventManager.TriggerEvent("SFX_ScoreAppear", 0);
        if (data.BaseValObj) { data.BaseValObj.SetActive(true); }
        data.ScoreValObj.SetActive(true);
        StartCoroutine(ScoreFlip(data.ScoreValStr, data.ScoreVal));
    }

    IEnumerator ScoreFlip(TMP_Text text, int realScore)
    {
        float duration = DELAY;
        const float SECONDS = 0.1f;
        int toCalc = 0;
        while (duration > 0)
        {
            duration -= SECONDS;
            int step = realScore / ((int)(DELAY / SECONDS) + 1);
            if (step != 0) { EventManager.TriggerEvent("SFX_Typewriter", 0); }
            toCalc += step;
            if (toCalc > realScore) { break; }
            text.text = "" + toCalc;
            yield return new WaitForSeconds(SECONDS);
        }
        text.text = "" + realScore;
    }
}
