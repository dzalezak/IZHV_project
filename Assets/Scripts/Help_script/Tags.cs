using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tags {
    public static string WALL = "Wall";
    public static string FRUIT = "Fruit";
    public static string MUSHROOM = "Mushroom";
    public static string TAIL = "Tail";

    public static string G_APPLE = "GoldenApple";

}

public class Metrics {
    public static float NODE = 0.5f;
}

public enum PlayerDirection {
    LEFT = 0,
    UP = 1,
    RIGHT = 2,
    DOWN = 3,
    COUNT = 4
}