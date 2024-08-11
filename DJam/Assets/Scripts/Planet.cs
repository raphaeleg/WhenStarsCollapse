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
    [SerializeField] private float stagesTimeThreshold = 10f;    // Time between each stage
    [SerializeField] bool isCuring = false;

    [SerializeField] private float localTimer = 10f;

    [SerializeField] GameEventSO BlackholeAdded;
    [SerializeField] GameEventSO KillSpawnedPlanet;
    [SerializeField] GameEventSO SuccessfulSpawn;

    private const int RATE_OF_ROTATION = 10;
    [SerializeField] private Animator animator;
    private const int VISIBILITY_DELAY = 4;
    private bool passInitialSpawnCheck = false;

    private void Start()
    {
        gameObject.transform.localScale = new Vector3(2,2,2);
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
            GameObject.Find("ScoreManager").GetComponent<HighScore>().stars++;
            return;
        } 
        
        localTimer = stagesTimeThreshold;
        if (isCuring) {                 // curing gives another chance
            isCuring = false;
            ChangeState(false);
            if (state == PlanetStates.WHITEDWARF) { BecomeWhiteDwarf(); }
            return;
        }
        ChangeState(true);
        if (state == PlanetStates.BLACKHOLE) { BecomeBlackHole(); }
    }

    private void RotateBlackHole()
    {
        gameObject.transform.Rotate(0, 0, Time.deltaTime * RATE_OF_ROTATION);
    }

    private void UpdateWhiteDwarf()
    {
        // 20 more seconds until disappearing
        if (localTimer > 0) { return; }
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (state != PlanetStates.BLACKHOLE) { return; }
        GameObject collider = other.gameObject;

        Planet p = collider.GetComponent<Planet>();
        if (p != null) {
            if (p.state == PlanetStates.BLACKHOLE) { return; }
            p.ShrinkUntilDestroy(); 
        }
        else { collider.GetComponent<TakeItem>().ShrinkUntilDestroy(); }
    }

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

    public void OnDrop(PointerEventData eventData)
    {
        if (IsNotCurable()) { return; }

        GameObject dropped = eventData.pointerDrag;
        if (IsRightCure(dropped.name))
        {
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
    private bool IsRightCure(string item) { return item == cureMap[type]; }
 
    public void Cure() {
        ChangeState(false);
        isCuring = true; 
    }
    private void ChangeState(bool isIncrease)
    {
        if (isIncrease) { state++; }
        else { state--; }
        animator.SetInteger("Stage", (int)state);
    }
    private void BecomeWhiteDwarf()
    {
        animator.SetTrigger("Dwarf");
    }
    public void BecomeBlackHole()
    {
        GameObject.Find("ScoreManager").GetComponent<HighScore>().blackHoles++;
        animator.SetTrigger("Explode");
        BlackholeAdded.Raise();
    }
}
