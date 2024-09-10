using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance { get; private set; }
    [SerializeField] private List<CursorAnimation> cursorAnimationList;
    private CursorAnimation cursorAnimation;

    private int currentFrame;
    private float frameTimer;
    private int frameCount;

    public enum CursorType { Arrow, Grab, Click }

    private void Awake()
    {
        Instance = this;
    }
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
