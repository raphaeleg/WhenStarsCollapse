using System.Collections;
using TMPro;
using UnityEngine;

namespace Tutorial
{
    /// <summary>
    /// Shows the text in a typewriter effect.
    /// </summary>
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
            if (_tmpProText is not null) 
            {
                writer = _text;
                _tmpProText.text = "";

                if (coroutine is not "") 
                { 
                    StopCoroutine(coroutine); 
                }
                coroutine = "TypeWriterTMP";
                StartCoroutine(coroutine);
            }
        }
        private void PlaySFX(int len, char c)
        {
            if (len % SFX_FREQ is 0 && c is not ' ')
            {
                EventManager.TriggerEvent("SFX_Typewriter", 1);
            }
        }

        IEnumerator TypeWriterTMP()
        {
            _tmpProText.text = isLeadingCharBeforeDelay ? leadingChar : "";

            yield return new WaitForSeconds(START_DELAY);
            
            int len = _tmpProText.text.Length;
            foreach (char c in writer)
            {
                if (len > 0)
                {
                    _tmpProText.text = _tmpProText.text.Substring(0, len - leadingChar.Length);
                }
                PlaySFX(len, c);
                _tmpProText.text += c;
                _tmpProText.text += leadingChar;
                yield return new WaitForSeconds(TIME_BTW_CHAR);
            }

            if (leadingChar is not "")
            {
                _tmpProText.text = _tmpProText.text.Substring(0, len - leadingChar.Length);
            }

            coroutine = "";
        }
    }
}