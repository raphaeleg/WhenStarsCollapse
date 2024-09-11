using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay_Guide : MonoBehaviour
{
    private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }
}
