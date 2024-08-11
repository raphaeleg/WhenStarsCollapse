using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Planet;

public class Planet : MonoBehaviour, IDropHandler
{
    public enum PlanetStates { WHITEDWARF, INITIAL, STAGE1, STAGE2, STAGE3, BLACKHOLE};
    [SerializeField] PlanetStates state = PlanetStates.INITIAL;
    public enum PlanetType {BLUE, GREEN, PINK};
    [SerializeField] PlanetType type = PlanetType.BLUE;
    public static Dictionary<PlanetType, string> cureMap = new()
        {
            { PlanetType.PINK, "CureA" },
            { PlanetType.GREEN, "CureB" },
            { PlanetType.BLUE, "CureC" }
        };
    [SerializeField] private float stagesTimeThreshold = 15f;    // Time between each stage
    [SerializeField] bool isCuring = false;
    [SerializeField] GameObject CuringParticles;

    [SerializeField] private float localTimer;

    [SerializeField] GameEventSO BlackholeAdded;
    [SerializeField] GameEventSO KillSpawnedPlanet;
    [SerializeField] GameEventSO SuccessfulSpawn;

    private const int RATE_OF_ROTATION = 10;
    [SerializeField] private Animator animator;
    private const int VISIBILITY_DELAY = 4;
    private bool passInitialSpawnCheck = false;
    [SerializeField] private GameObject sickParticles;

    [SerializeField] HighScore highScore;

    public Sprite[] spawnAnim;
    public Sprite[] getSickAnim;
    public Sprite[] whiteDwardDestroyAnim;
    public Sprite[] curedAnim;
    [SerializeField] GameObject BlackholeOverlay;

    private void Start()
    {
        localTimer = stagesTimeThreshold;
        gameObject.transform.localScale = new Vector3(2,2,2);
        StartCoroutine(StartAnim());
    }

    private void Update()
    {
        UpdateTimer();

        switch (state)
        {
            case PlanetStates.WHITEDWARF:
                UpdateWhiteDwarf();
                break;
            case PlanetStates.BLACKHOLE:
                RotateBlackHole();
                break;
            default:
                UpdateState();
                break;
        }
    }

    private void UpdateTimer() { if (localTimer > 0) { localTimer -= Time.deltaTime; } }

    private void UpdateState() {
        if (localTimer > 0) {   // still waiting
            if (passInitialSpawnCheck) { return; }
            if (localTimer > VISIBILITY_DELAY) {  return; }
            
            SuccessfulSpawn.Raise();
            passInitialSpawnCheck = true;
            highScore.stars++;
            return;
        } 
        
        localTimer = stagesTimeThreshold;
        if (isCuring) {                 // curing gives another chance
            isCuring = false;
            CuringParticles.SetActive(false);
            ChangeState(false);
            if (state == PlanetStates.WHITEDWARF) { BecomeWhiteDwarf(); }
            return;
        }
        ChangeState(true);
        if (state == PlanetStates.STAGE3) { SickParticles(true); }
        else { SickParticles(false); }
        if (state == PlanetStates.BLACKHOLE) { BecomeBlackHole(); }
    }

    private void ChangeState(bool isIncrease)
    {
        if (isIncrease) {
            state++;
            if (state == PlanetStates.STAGE1)
            {
                StartCoroutine(GetSick());
            }
            else if (state == PlanetStates.STAGE2)
            {
                StartCoroutine(GetBig());
            }
            else
                animator.SetInteger("Stage", (int)state);
        }
        else {
            state--;
            if (state == PlanetStates.WHITEDWARF)
            {
                StartCoroutine(GetCured());
            }
            else
                animator.SetInteger("Stage", (int)state);
        }
    }

    #region BlackHole Functionality
    public void BecomeBlackHole()
    {
        StartCoroutine(ExplosionSFX());
        BackgroundMusic.instance.PlaySFX("SFX_Wind_Loop");
        highScore.blackHoles++;
        animator.SetTrigger("Explode");
        BlackholeAdded.Raise();
    }
    private void RotateBlackHole()
    {
        gameObject.transform.Rotate(0, 0, Time.deltaTime * RATE_OF_ROTATION);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (state != PlanetStates.BLACKHOLE) { return; }

        var blackHoleEat = Instantiate(BlackholeOverlay);
        blackHoleEat.GetComponent<AnimationNoLoop>().destroySelf = true;
        blackHoleEat.transform.SetParent(transform);
        blackHoleEat.transform.localScale = new Vector3(2, 2, 2);
        blackHoleEat.transform.localPosition = Vector3.zero;

        GameObject collider = other.gameObject;
        Planet p = collider.GetComponent<Planet>();
        if (p != null)
        {
            if (p.state == PlanetStates.BLACKHOLE) { return; }
            p.ShrinkUntilDestroy();
        }
        else { collider.GetComponent<TakeItem>().ShrinkUntilDestroy(); }
        StartCoroutine(MoveCollider(collider));
    }

    IEnumerator MoveCollider(GameObject collider)
    {
        float time = 0.5f;
        while (time > 0 && collider != null)
        {
            var directionToCollider = collider.transform.position - transform.position;
            collider.transform.localPosition -= 5f * directionToCollider;
            yield return new WaitForSeconds(0.1f);
        }
    }
    #endregion

    #region WhiteDwarf Functionality
    private void BecomeWhiteDwarf()
    {
        highScore.whiteDwarfs++;
        animator.SetTrigger("Dwarf");
    }

    private void UpdateWhiteDwarf()
    {
        // 20 more seconds until disappearing
        if (localTimer > 0) { return; }
        StartCoroutine(WhiteDwarfDestroy());
    }
    IEnumerator WhiteDwarfDestroy()
    {
        transform.Find("Image").GetComponent<Animator>().enabled = false;
        transform.Find("Image").localPosition = new Vector3(9.2f, 16.4f, 0);
        transform.Find("Image").localScale = new Vector3(5, 5, 5);
        for (int i = 0; i < whiteDwardDestroyAnim.Length; i++)
        {
            transform.Find("Image").GetComponent<Image>().sprite = whiteDwardDestroyAnim[i];
            yield return new WaitForSeconds(0.05f);
        }
        Destroy(gameObject);
    }
    #endregion

    # region Behaviour with Cures
    public void OnDrop(PointerEventData eventData)
    {
        if (IsNotCurable()) { return; }

        GameObject dropped = eventData.pointerDrag;
        if (IsRightCure(dropped.name))
        {
            BackgroundMusic.instance.PlaySFX("SFX_Boost");
            if (dropped.name == "CureA")
            { GameObject.Find("A_List").GetComponent<CureStat>().totalCures--; }
            if (dropped.name == "CureB")
            { GameObject.Find("B_List").GetComponent<CureStat>().totalCures--; }
            if (dropped.name == "CureC")
            { GameObject.Find("C_List").GetComponent<CureStat>().totalCures--; }

            Cure();
        }

    }
    private bool IsNotCurable()
    {
        return state == PlanetStates.BLACKHOLE || state == PlanetStates.WHITEDWARF || state == PlanetStates.INITIAL || isCuring;
    }
    private void SickParticles(bool active)
    {
        sickParticles.SetActive(active);
    }
    private bool IsRightCure(string item) { return item == cureMap[type]; }

    public void Cure()
    {
        ChangeState(false);
        isCuring = true;
        CuringParticles.SetActive(true);
    }
    #endregion

    #region Collide With BlackHole

    public void ShrinkUntilDestroy()
    {
        StartCoroutine(Shrink());
    }

    IEnumerator Shrink()
    {
        Transform t = gameObject.transform;
        while (t.localScale.x > 0f)
        {
            t.localScale -= new Vector3(0.1f, 0.1f, 0f);
            yield return new WaitForSeconds(0.1f);
        }
        if (state == PlanetStates.INITIAL) { KillSpawnedPlanet.Raise(); }
        Destroy(gameObject);
    }

    #endregion

    #region VFX
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
    #endregion
}
