using RTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Persistent game data.
/// </summary>
public class GameData : ScriptableData<GameData>
{
    /// <summary>
    /// Current rocket capacity.
    /// </summary>
    [Range(3, 10)]
    public int rocketCapacity = 3;

    /// <summary>
    /// Current shield capacity.
    /// </summary>
    [Range(40, 100)]
    public int shieldCapacity = 40;

    /// <summary>
    /// Interval before shield recharge.
    /// </summary>
    public float shieldRechargeRate = 10;

    /// <summary>
    /// Highscore table.
    /// </summary>
    public List<ScoreData> scores = new List<ScoreData>();
}

[System.Serializable]
public class ScoreData
{
    public string name;
    public int score;
}

/// <summary>
/// Temporary game data.
/// </summary>
public static class SessionData
{
    /// <summary>
    /// Player's lives.
    /// </summary>
    public static int lives = 3;

    /// <summary>
    /// Player's coins.
    /// </summary>
    public static int coins = 0;

    /// <summary>
    /// Player's score.
    /// </summary>
    public static int score;

    /// <summary>
    /// Current rocket.
    /// </summary>
    public static int rocket;

    /// <summary>
    /// Enemy kill count.
    /// </summary>
    public static int enemiesKilled;

    /// <summary>
    /// Initialize session data. Should be called once before starting a new game.
    /// </summary>
    public static void Initialize()
    {
        lives = 3;
        score = 0;
        enemiesKilled = 0;
        coins = 0;
        rocket = GameData.Instance.rocketCapacity;
    }

    /// <summary>
    /// Reset session data. Should be called at the beginning of each round.
    /// </summary>
    public static void Reset()
    {
        //rocket = GameData.Instance.rocketCapacity;
    }
}