using System;

public static class EventManager {
    public static event Action<int> ScoreUpdated;
    public static void OnScoreUpdated(int score) => ScoreUpdated?.Invoke(score);

    public static event Action<int> EnemyDestroyed;
    public static void OnEnemyDestroyed(int score) => EnemyDestroyed?.Invoke(score);

    public static event Action<int> CoinCollected;
    public static void OnCoinCollected(int value) => CoinCollected?.Invoke(value);
}