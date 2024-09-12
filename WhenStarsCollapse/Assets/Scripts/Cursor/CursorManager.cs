using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance { get; private set; }
    [SerializeField] private List<CursorAnimation> cursorAnimationList;
    private CursorAnimation cursorAnimation;

    private int currentFrame;
    private float frameTimer;
    private int frameCount;
    #region EventManager
    private Dictionary<string, Action<int>> SubscribedEvents;

    private void Awake()
    {
        Instance = this;
        SubscribedEvents = new() {
            { "Rune_SetDragging", SetGrab },
        };
    }
    private void OnEnable()
    {
        StartCoroutine("DelayedSubscription");
    }
    private IEnumerator DelayedSubscription()
    {
        yield return new WaitForSeconds(0.0001f);
        foreach (var pair in SubscribedEvents)
        {
            EventManager.StartListening(pair.Key, pair.Value);
        }
    }

    private void OnDisable()
    {
        foreach (var pair in SubscribedEvents)
        {
            EventManager.StopListening(pair.Key, pair.Value);
        }
    }
    #endregion

    public enum CursorType { Arrow, Grab, Click }

    private void Start()
    {
        SetActiveCursorType(CursorType.Arrow);
    }

    private void Update()
    {
        frameTimer -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0)) {
            SetActiveCursorType(CursorType.Click);
        }

        if (frameTimer > 0f) { return; }

        frameTimer = cursorAnimation.frameRate;
        currentFrame = (currentFrame + 1) % frameCount;
        if (currentFrame == 0 && cursorAnimation.playOnce) { 
            SetActiveCursorType(CursorType.Arrow); 
        }
        Cursor.SetCursor(cursorAnimation.textureArray[currentFrame], cursorAnimation.offset, CursorMode.ForceSoftware);
    }

    private void SetActiveCursorAnimation(CursorAnimation cursorAnimation)
    {
        this.cursorAnimation = cursorAnimation;
        currentFrame = 0;
        frameCount = cursorAnimation.textureArray.Length;
        frameTimer = cursorAnimation.frameRate;
    }

    public void SetActiveCursorType(CursorType cursorType)
    {
        SetActiveCursorAnimation(GetCursorAnimation(cursorType));
    }

    private CursorAnimation GetCursorAnimation(CursorType cursorType)
    {
        return cursorAnimationList.First(item => item.cursorType == cursorType);
    }

    public void SetGrab(int val)
    {
        CursorType toSwitch = val != 0 ? CursorType.Grab : CursorType.Arrow;
        SetActiveCursorType(toSwitch);
    }

    [CreateAssetMenu]
    public class CursorAnimation : ScriptableObject
    {
        public CursorType cursorType;
        public bool playOnce = false;
        public Texture2D[] textureArray;
        public float frameRate = 0.1f;
        public Vector2 offset;
    }
}
