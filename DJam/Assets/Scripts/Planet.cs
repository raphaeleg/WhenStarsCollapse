using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] PlanetRuntimeSet PlanetList;
    [SerializeField] private float stagesTimeThreshold = 10f;    // Time between each stage
    [SerializeField] bool isCuring = false;

    [SerializeField] private float localTimer = 10f;

    [SerializeField] GameEventSO BlackholeAdded;

    private const int RATE_OF_ROTATION = 10;
    private Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
        PlanetList.Add(gameObject);

        GameObject.Find("ScoreManager").GetComponent<HighScore>().stars++;
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
        if (localTimer > 0) { return; } // still waiting
        
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
        if (collider.GetComponent<Planet>() != null)
        {
            PlanetList.Planets.Remove(collider);
        }
        Destroy(collider);
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
        //planetImage.sprite = planetImageList[(int)state];
    }
    private void BecomeWhiteDwarf()
    {
        animator.SetTrigger("Dwarf");
        RemoveFromList();
    }
    public void BecomeBlackHole()
    {
        GameObject.Find("ScoreManager").GetComponent<HighScore>().blackHoles++;
        animator.SetTrigger("Explode");
        RemoveFromList();
        BlackholeAdded.Raise();
    }
    public void RemoveFromList() { PlanetList.Remove(gameObject); }
}
