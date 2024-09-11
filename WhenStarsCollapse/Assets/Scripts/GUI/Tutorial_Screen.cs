using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Screen : MonoBehaviour
{
    private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }
}
