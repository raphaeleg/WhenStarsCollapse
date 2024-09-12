using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tutorial
{
    public class TypeWriterEffect : MonoBehaviour
    {
        private TMP_Text _tmpProText;
        private string writer;
        private const int SFX_FREQ = 3;

        const float START_DELAY = 0f;
        [SerializeField] const float TIME_BTW_CHAR = 0.03f;
        private string leadingChar = "";
        const bool isLeadingCharBeforeDelay = false;
        string coroutine = "";

        public void Restart(string _text)
        {
            _tmpProText = GetComponent<TMP_Text>()!;
            if (_tmpProText != null) {
                writer = _text;
                _tmpProText.text = "";

                if (coroutine != "") { StopCoroutine(coroutine); }
                coroutine = "TypeWriterTMP";
                StartCoroutine(coroutine);
            }
        }

        IEnumerator TypeWriterTMP()
        {
            _tmpProText.text = isLeadingCharBeforeDelay ? leadingChar : "";

            yield return new WaitForSeconds(START_DELAY);
            
            foreach (char c in writer)
            {
                if (_tmpProText.text.Length > 0)
                {
                    _tmpProText.text = _tmpProText.text.Substring(0, _tmpProText.text.Length - leadingChar.Length);
                }
                if (_tmpProText.text.Length % SFX_FREQ == 0 && c != ' ') { EventManager.TriggerEvent("SFX_Typewriter", 1); }
                _tmpProText.text += c;
                _tmpProText.text += leadingChar;
                yield return new WaitForSeconds(TIME_BTW_CHAR);
            }

            if (leadingChar != "")
            {
                _tmpProText.text = _tmpProText.text.Substring(0, _tmpProText.text.Length - leadingChar.Length);
            }

            coroutine = "";
        }
    }
}