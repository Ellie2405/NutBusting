using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    public const string PLAYER_PROJECTILE_TAG = "PlayerProj";

    //Board
    public const int BOARD_SLICES = 36;
    public const float TURRET_SPACING = Mathf.PI / 6;
    public const float MIN_ROTATION = 360 / BOARD_SLICES;
    public const int CLOCKWISE = 1;
    public const int COUNTERCLOCKWISE = -1;

    public const int MAX_CURRENCY = 100;
}
