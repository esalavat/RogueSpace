using System;

public static class EventManager {
    public static event Action<int> ScoreUpdated;
    public static void OnScoreUpdated(int score) => ScoreUpdated?.Invoke(score);
}