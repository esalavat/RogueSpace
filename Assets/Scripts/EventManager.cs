using System;

public static class EventManager {
    public static event Action<int> OnScoreUpdated;
    public static void ScoreUpdated(int score) => OnScoreUpdated?.Invoke(score);

    public static event Action<int> OnCreditsUpdated;
    public static void CreditsUpdated(int credits) => OnCreditsUpdated?.Invoke(credits);

    public static event Action<int> OnEnemyDestroyed;
    public static void EnemyDestroyed(int score) => OnEnemyDestroyed?.Invoke(score);

    public static event Action<int> OnCoinCollected;
    public static void CoinCollected(int value) => OnCoinCollected?.Invoke(value);

    public static event Action<string> OnMessage;
    public static void Message(string message) => OnMessage?.Invoke(message);

    public static event Action<int> OnLifeUpdated;
    public static void LifeUpdated(int life) => OnLifeUpdated?.Invoke(life);
}