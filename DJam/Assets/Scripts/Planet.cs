using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Planet;

public class Planet : MonoBehaviour, IDropHandler
{
    // States: Normal, Symptom, WhiteStar, BlackHole
    public enum PlanetStates { WHITEDWARF, INITIAL, STAGE1, STAGE2, STAGE3, BLACKHOLE};
    [SerializeField] PlanetStates state = PlanetStates.INITIAL;
    public enum PlanetType {BLUE, GREEN, PINK};
    [SerializeField] PlanetType type = PlanetType.BLUE;
    public static Dictionary<PlanetType, string> cureMap = new()
        {
            { PlanetType.BLUE, "CureA" },
            { PlanetType.GREEN, "CureB" },
            { PlanetType.PINK, "CureC" }
        };
    [SerializeField] PlanetRuntimeSet PlanetList;
    [SerializeField] private float stagesTimeThreshold = 10f;    // Time between each stage
    [SerializeField] bool isCuring = false;

    [SerializeField] private float localTimer = 10f;

    [SerializeField] GameEventSO BlackholeAdded;

    private Image planetImage;
    public List<Sprite> planetImageList;


    private void Start()
    {
        planetImage = GetComponent<Image>();
        PlanetList.Add(gameObject);

        GameObject.Find("ScoreManager").GetComponent<HighScore>().stars++;
    }

    private void Update()
    {
        UpdateTimer();

        switch (state)
        {
            case PlanetStates.WHITEDWARF:
                UpdateFinalState();
                break;
            case PlanetStates.BLACKHOLE:
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
            state--;
            planetImage.sprite = planetImageList[(int)state];
            if (state == PlanetStates.WHITEDWARF) { RemoveFromList(); }
            return;
        }
        state++;
        planetImage.sprite = planetImageList[(int)state];
        if (state == PlanetStates.BLACKHOLE) { BecomeBlackHole(); }
    }

    private void UpdateFinalState()
    {
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
        if (state == PlanetStates.BLACKHOLE || state == PlanetStates.WHITEDWARF) { return; }

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

    private bool IsRightCure(string item) { return item == cureMap[type]; }
    public void BecomeBlackHole()
    {
        GameObject.Find("ScoreManager").GetComponent<HighScore>().blackHoles++;
        RemoveFromList();
        BlackholeAdded.Raise();
    }
    public void RemoveFromList() { PlanetList.Remove(gameObject); }
 
    public void Cure() { 
        state--;
        planetImage.sprite = planetImageList[(int)state];
        isCuring = true; 
    }

}
