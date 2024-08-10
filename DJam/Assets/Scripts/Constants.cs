using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    // System
    public const int FRAME_RATE = 60;
    public const float DELTA_TIME = 1 / 60f;

    // Scene
    public const string MAIN_MENU_NAME = "MainMenu";
    public const string GAMEPLAY_SCENE_NAME = "Gameplay";
    public const string WIN_SCENE_NAME = "WinScreen";

    // Game
    public const int MAX_HEALTH = 200;
    public const int MAX_WATT = 100;
    public const int MAX_DEATH = 10;

    // Light
    public const float MIN_LIGHT_SCALE = 2f;
    public const float MAX_LIGHT_SCALE = 8.0f;
    public const float CHECK_HOSTILE_LIGHT_INTERVAL = 0.2f;
    public const int HOSTILE_LIGHT_DAMAGE_VALUE = 1;
}
