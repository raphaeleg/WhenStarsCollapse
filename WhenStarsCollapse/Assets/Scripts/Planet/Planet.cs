using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Planet : StateMachine
{
    public enum PlanetType { BLUE, GREEN, PINK };
    [SerializeField] PlanetType type = PlanetType.BLUE;
    public static Dictionary<PlanetType, string> cureMap = new()
        {
            { PlanetType.PINK, "CureA" },
            { PlanetType.GREEN, "CureB" },
            { PlanetType.BLUE, "CureC" }
        };
    private bool IsRightCure(string item) { return item == cureMap[type]; }

    public void Start()
    {
        SetState(new Begin(this));
    }
    public void Update()
    {
        StartCoroutine(State.Update());
    }
    public virtual IEnumerator OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (!IsRightCure(dropped.name)) { yield break; }

        // TODO: Minus from runebar list
        StartCoroutine(State.Heal()); 
    }

    public void ShrinkUntilDestroy()
    {
        StartCoroutine(State.Shrink());
    }

    public void OnDestroy()
    {
        Destroy(gameObject);
    }

    /*#region VFX
    IEnumerator StartAnim()
    {
        transform.Find("Image").GetComponent<Animator>().enabled = false;
        transform.Find("Image").localScale = new Vector3(2.35f, 2.35f, 2.35f);
        transform.Find("Image").localPosition = new Vector3(3.1f, -2.9f, 0);
        for (int i = 0; i < spawnAnim.Length; i++)
        {
            if (i == 10)
                BackgroundMusic.instance.PlaySFX("SFX_Force_Field");
            transform.Find("Image").GetComponent<Image>().sprite = spawnAnim[i];
            yield return new WaitForSeconds(0.05f);
        }
        transform.Find("Image").GetComponent<Animator>().enabled = true;
        transform.Find("Image").localScale = new Vector3(1, 1, 1);
        transform.Find("Image").localPosition = Vector3.zero;
    }
    IEnumerator GetSick()
    {
        BackgroundMusic.instance.PlaySFX("SFX_Force_Field");
        transform.Find("Image").GetComponent<Animator>().enabled = false;
        transform.Find("Image").localScale = new Vector3(2.35f, 2.35f, 2.35f);
        transform.Find("Image").localPosition = new Vector3(3.1f, -3.1f, 0);
        for (int i = 0; i < getSickAnim.Length; i++)
        {
            transform.Find("Image").GetComponent<Image>().sprite = getSickAnim[i];
            yield return new WaitForSeconds(0.05f);
        }
        transform.Find("Image").GetComponent<Animator>().enabled = true;
        animator.SetInteger("Stage", (int)state);
        transform.Find("Image").localScale = new Vector3(1, 1, 1);
        transform.Find("Image").localPosition = Vector3.zero;
    }
    IEnumerator GetCured()
    {
        //yield return new WaitForSeconds(1);
        transform.Find("Image").GetComponent<Animator>().enabled = false;
        transform.Find("Image").localScale = new Vector3(2.35f, 2.35f, 2.35f);
        transform.Find("Image").localPosition = new Vector3(3.1f, -3.1f, 0);
        for (int i = 0; i < curedAnim.Length; i++)
        {
            transform.Find("Image").GetComponent<Image>().sprite = curedAnim[i];
            yield return new WaitForSeconds(0.05f);
        }
        transform.Find("Image").GetComponent<Animator>().enabled = true;
        animator.SetInteger("Stage", (int)state);
        transform.Find("Image").localScale = new Vector3(1, 1, 1);
        transform.Find("Image").localPosition = Vector3.zero;
    }
    IEnumerator GetBig()
    {
        BackgroundMusic.instance.PlaySFX("SFX_Force_Field");
        transform.Find("Image").GetComponent<Animator>().enabled = false;
        transform.Find("Image").GetComponent<Image>().color = Color.black;
        yield return new WaitForSeconds(0.2f);
        transform.Find("Image").GetComponent<Image>().color = Color.white;
        yield return new WaitForSeconds(0.3f);
        transform.Find("Image").GetComponent<Image>().color = Color.black;
        yield return new WaitForSeconds(0.2f);
        transform.Find("Image").GetComponent<Image>().color = Color.white;
        yield return new WaitForSeconds(0.3f);
        transform.Find("Image").GetComponent<Image>().color = Color.black;
        yield return new WaitForSeconds(0.3f);
        transform.Find("Image").GetComponent<Animator>().enabled = true;
        transform.Find("Image").GetComponent<Image>().color = Color.white;
        animator.SetInteger("Stage", (int)state);
    }

    IEnumerator ExplosionSFX()
    {
        yield return new WaitForSeconds(0.25f);
        BackgroundMusic.instance.PlaySFX("SFX_Explosion");
    }
    #endregion*/
}
