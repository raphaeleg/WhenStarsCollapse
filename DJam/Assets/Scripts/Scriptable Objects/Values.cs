using System;
using UnityEngine;

[CreateAssetMenu]
public class Values : ScriptableObject
{
    [SerializeField] Values initial;

    [Header("Player")]
    #region Player
    [SerializeField] bool allowInput;
    [SerializeField] bool allowMove;
    public Vector3 playerPos;
    public bool isMoving;
    public bool isFocusing;
    [Range(1, Constants.MAX_HEALTH)]
    public int playerHealth;
    public int deathCounter;
    #endregion

    [Space(10)]

    [Range(1, Constants.MAX_WATT)]
    public int sliderWatt;

    public Vector3 exitPos;

    public bool GetAllowInput() => allowInput;

    public void SetAllowInput(bool state)
    {
        allowInput = state;
        allowMove = state;
    }

    public bool GetAllowMove() => allowInput ? allowMove : false;

    public void SetAllowMove(bool state) => allowMove = state;

    public void Reset()
    {
        allowInput = initial.allowInput;
        allowMove = initial.allowMove;
        playerPos = initial.playerPos;
        isMoving = initial.isMoving;
        isFocusing = initial.isFocusing;
        playerHealth = initial.playerHealth;
        sliderWatt = initial.sliderWatt;
        deathCounter = initial.deathCounter;
        exitPos = initial.exitPos;
    }

    public Vector3 InitialPlayerPos() => initial.playerPos;

    public int InitialSliderWatt() => initial.sliderWatt;
}
