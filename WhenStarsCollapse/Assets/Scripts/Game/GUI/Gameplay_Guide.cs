using UnityEngine;

/// <summary>
/// Ensures that the Guide in the Gameplay Scene plays when the game is paused.
/// </summary>
public class Gameplay_Guide : MonoBehaviour
{
    private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }
}
